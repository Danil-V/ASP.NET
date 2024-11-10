using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.WebHost.Controllers;
using PromoCodeFactory.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PromoCodeFactory.UnitTests.WebHost.Controllers.Partners
{
    public class SetPartnerPromoCodeLimitAsyncTests {
        //TODO: Add Unit Tests

        // Test 1:
        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_PartnerNotFound_Return404() {
            // Arrange: задаем набор начальных данных:
            var partnerId = Guid.NewGuid();
            var request = CreateRequest();
            // Создаем мок для репозитория, который возвращает null при запросе партнера по id:
            var repositoryMock = new Mock<IRepository<Partner>>();
            repositoryMock.Setup(repo => repo.GetByIdAsync(partnerId)).ReturnsAsync((Partner)null);
            // Создаем контроллер с подставленным репозиторием:
            var controller = CreateController(repositoryMock.Object);

            // Act: вызываем тестируемый метод:
            var result = await controller.SetPartnerPromoCodeLimitAsync(partnerId, request);

            // Assert: проверяем, что возвращен 404 Not Found:
            result.Should().BeOfType<NotFoundResult>();
        }


        // Test 2:
        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_PartnerBanned_Return404() {
            // Arrange: задаем набор начальных данных:
            var partner = CreatePartner(isActive: false);   // Создаем заблокированного партнера.
            var request = CreateRequest();
            var repositoryMock = new Mock<IRepository<Partner>>();
            repositoryMock.Setup(repo => repo.GetByIdAsync(partner.Id)).ReturnsAsync(partner);
            var controller = CreateController(repositoryMock.Object);

            // Act: вызываем метод для заблокированного партнера:
            var result = await controller.SetPartnerPromoCodeLimitAsync(partner.Id, request);

            // Assert: проверяем, что возвращен 400 Bad Request с нужным сообщением:
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().Be("Данный партнер не активен");
        }

        // Test 3:
        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_LimitHasAlreadySet_ResetPromoCodeCount() {
            // Arrange: создаем партнера с выданными промокодами и активным лимитом:
            var partner = CreatePartnerWithLimit(issuedPromoCodes: 5);
            var request = CreateRequest(limit: 10);
            var repositoryMock = new Mock<IRepository<Partner>>();
            repositoryMock.Setup(repo => repo.GetByIdAsync(partner.Id)).ReturnsAsync(partner);
            var controller = CreateController(repositoryMock.Object);

            // Act: вызываем метод для установки нового лимита:
            await controller.SetPartnerPromoCodeLimitAsync(partner.Id, request);

            // Assert: проверяем, что количество выданных промокодов обнулено:
            partner.NumberIssuedPromoCodes.Should().Be(0);
        }

        // Test 4:
        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_TurnOffPreviousLimit() {
            // Arrange: создаем партнера с активным лимитом:
            var partner = CreatePartnerWithLimit();
            var request = CreateRequest(limit: 15);
            var repositoryMock = new Mock<IRepository<Partner>>();
            repositoryMock.Setup(repo => repo.GetByIdAsync(partner.Id)).ReturnsAsync(partner);
            var controller = CreateController(repositoryMock.Object);

            // Act: устанавливаем новый лимит для партнера:
            await controller.SetPartnerPromoCodeLimitAsync(partner.Id, request);

            // Assert: проверяем, что предыдущий лимит теперь имеет дату завершения:
            var previousLimit = partner.PartnerLimits.FirstOrDefault(x => x.CancelDate.HasValue);
            previousLimit.Should().NotBeNull();
            previousLimit.CancelDate.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
        }

        // Test 5:
        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_LimitLessOrEqualToZero() {
            // Arrange: создаем партнера и запрос с недопустимым лимитом:
            var partner = CreatePartner();
            var request = CreateRequest(limit: 0);
            var repositoryMock = new Mock<IRepository<Partner>>();
            repositoryMock.Setup(repo => repo.GetByIdAsync(partner.Id)).ReturnsAsync(partner);
            var controller = CreateController(repositoryMock.Object);

            // Act: вызываем метод с запросом, где лимит меньше или равен 0:
            var result = await controller.SetPartnerPromoCodeLimitAsync(partner.Id, request);

            // Assert: проверяем, что возвращен 400 Bad Request с нужным сообщением:
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().Be("Лимит должен быть больше 0");
        }

        // Test 6:
        [Fact]
        public async Task SetPartnerPromoCodeLimitAsync_SaveNewLimit() {
            // Arrange: создаем партнера и запрос с допустимым лимитом:
            var partner = CreatePartner();
            var request = CreateRequest(limit: 10, endDate: DateTime.Now.AddDays(10));
            // Мок репозитория, который подтверждает обновление партнера:
            var repositoryMock = new Mock<IRepository<Partner>>();
            repositoryMock.Setup(repo => repo.GetByIdAsync(partner.Id)).ReturnsAsync(partner);
            repositoryMock.Setup(repo => repo.UpdateAsync(partner)).Returns(Task.CompletedTask);
            var controller = CreateController(repositoryMock.Object);

            // Act: вызываем метод с запросом:
            await controller.SetPartnerPromoCodeLimitAsync(partner.Id, request);

            // Assert: проверяем, что партнер был обновлен и новый лимит добавлен:
            repositoryMock.Verify(repo => repo.UpdateAsync(partner), Times.Once);
            partner.PartnerLimits.Should().ContainSingle(x => x.Limit == request.Limit && x.EndDate == request.EndDate);
        }


        // Фабричные (вспомогательные) методы для создания данных:
        // Создает нового партнера с параметрами по умолчанию или заданными значениями:
        private Partner CreatePartner(bool isActive = true) {
            return new Partner {
                Id = Guid.NewGuid(),
                Name = "Test Partner",
                IsActive = isActive,
                PartnerLimits = new List<PartnerPromoCodeLimit>()
            };
        }

        // Создает партнера с установленным лимитом и указанным количеством выданных промокодов:
        private Partner CreatePartnerWithLimit(int issuedPromoCodes = 0) {
            var partner = CreatePartner();
            partner.NumberIssuedPromoCodes = issuedPromoCodes;
            partner.PartnerLimits.Add(new PartnerPromoCodeLimit {
                Id = Guid.NewGuid(),
                PartnerId = partner.Id,
                Limit = 10,
                CreateDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(1)
            });
            return partner;
        }

        // Создает запрос на установку лимита с параметрами по умолчанию или заданными значениями:
        private SetPartnerPromoCodeLimitRequest CreateRequest(int limit = 10, DateTime? endDate = null) {
            return new SetPartnerPromoCodeLimitRequest {
                Limit = limit,
                EndDate = endDate ?? DateTime.Now.AddDays(1)
            };
        }

        // Создает экземпляр контроллера с подставленным репозиторием:
        private PartnersController CreateController(IRepository<Partner> repository) {
            return new PartnersController(repository);
        }
    }
}