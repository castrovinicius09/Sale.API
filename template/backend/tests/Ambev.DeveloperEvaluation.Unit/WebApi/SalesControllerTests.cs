using Ambev.DeveloperEvaluation.Application.Sales.CancellSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;
using Ambev.DeveloperEvaluation.Unit.WebApi.TestData.Sales;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.WebApi
{
    public class SalesControllerTests
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly SalesController _controller;

        public SalesControllerTests()
        {
            _mediator = Substitute.For<IMediator>();
            _mapper = Substitute.For<IMapper>();
            _controller = new SalesController(_mediator, _mapper);
        }

        [Fact(DisplayName = "Given valid sale ID When GetSaleById called Then returns sale response")]
        public async Task GetSaleById_ValidId_ReturnsSale()
        {
            // Arrange
            var id = Guid.NewGuid();
            var resquest = new GetSaleByIdRequest { Id = id };
            var query = new GetSaleByIdQuery(resquest.Id);
            var getSaleByIdResult = SaleControllerTestData.GenerateValidGetSaleByIdResult();
            var apiResponse = new GetSaleByIdResponse { Id = id };

            _mediator.Send(query, Arg.Any<CancellationToken>()).Returns(getSaleByIdResult);

            _mapper.Map<GetSaleByIdQuery>(resquest).Returns(query);
            _mapper.Map<GetSaleByIdResponse>(getSaleByIdResult).Returns(apiResponse);

            // Act
            var result = await _controller.GetSaleById(id, CancellationToken.None);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            var response = okResult!.Value as ApiResponseWithData<GetSaleByIdResponse>;
            response!.Success.Should().BeTrue();
        }

        [Fact(DisplayName = "Given valid request When CreateSale called Then returns created response")]
        public async Task CreateSale_ValidRequest_ReturnsCreated()
        {
            // Arrange
            var request = SaleControllerTestData.GenerateValidCreateSaleRequest();
            var command = SaleHandlerTestData.GenerateValidCreateCommand();
            var result = new CreateSaleResult { Id = Guid.NewGuid() };

            _mapper.Map<CreateSaleCommand>(request).Returns(command);
            _mediator.Send(command, Arg.Any<CancellationToken>()).Returns(result);

            // Act
            var response = await _controller.CreateSale(request, CancellationToken.None);

            // Assert
            var createdResult = response as CreatedResult;
            createdResult.Should().NotBeNull();
            var payload = createdResult!.Value as ApiResponseWithData<Guid>;
            payload!.Success.Should().BeTrue();
            payload.Data.Should().Be(result.Id);
        }

        [Fact(DisplayName = "Given valid request When UpdateSale called Then returns updated response")]
        public async Task UpdateSale_ValidRequest_ReturnsUpdated()
        {
            // Arrange
            var request = SaleControllerTestData.GenerateValidUpdateSaleRequest();
            var command = SaleHandlerTestData.GenerateValidUpdateCommand();
            var result = new UpdateSaleResult { Id = Guid.NewGuid() };

            _mapper.Map<UpdateSaleCommand>(request).Returns(command);
            _mediator.Send(command, Arg.Any<CancellationToken>()).Returns(result);

            // Act
            var response = await _controller.UpdateSale(request, CancellationToken.None);

            // Assert
            var okResult = response as OkObjectResult;
            okResult.Should().NotBeNull();
            var payload = okResult!.Value as ApiResponseWithData<Guid>;
            payload!.Success.Should().BeTrue();
            payload.Data.Should().Be(result.Id);
        }

        [Fact(DisplayName = "Given valid ID When CancellSale called Then returns success response")]
        public async Task CancellSale_ValidId_ReturnsSuccess()
        {
            // Arrange
            var id = Guid.NewGuid();
            var command = new CancellSaleCommand { Id = Guid.NewGuid() };

            _mapper.Map<CancellSaleCommand>(id).Returns(command);
            _mediator.Send(command, Arg.Any<CancellationToken>()).Returns(true);

            // Act
            var response = await _controller.CancellSale(id, CancellationToken.None);

            // Assert
            var okResult = response as OkObjectResult;
            okResult.Should().NotBeNull();
            var payload = okResult!.Value as ApiResponse;
            payload!.Success.Should().BeTrue();
        }
    }
}
