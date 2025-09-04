using Ambev.DeveloperEvaluation.Support.WebApi.Sales;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.WebApi.Common;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional.Sales
{
    public class SalesFunctionalTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public SalesFunctionalTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "Should create sale without discount when item quantity is below 4")]
        public async Task CreateSale_WithLowQuantity_ShouldNotApplyDiscount()
        {
            var request = SaleControllerTestData.GenerateValidCreateSaleRequest();

            var response = await _client.PostAsJsonAsync("/api/sales", request);
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var result = await response.Content.ReadFromJsonAsync<ApiResponseWithData<Guid>>();
            result!.Success.Should().BeTrue();
        }

        //[Fact(DisplayName = "Should apply 20% discount when item quantity is between 10 and 20")]
        //public async Task CreateSale_WithMediumQuantity_ShouldApply20PercentDiscount()
        //{
        //    var request = new CreateSaleRequest
        //    {
        //        Items = new List<SaleItemDto>
        //    {
        //        new() { ProductId = Guid.NewGuid(), ProductName = "Produto B", Quantity = 10, UnitPrice = 50m }
        //    }
        //    };

        //    var response = await _client.PostAsJsonAsync("/api/sales", request);
        //    response.StatusCode.Should().Be(HttpStatusCode.Created);

        //    var saleId = (await response.Content.ReadFromJsonAsync<ApiResponseWithData<Guid>>())!.Data;

        //    var sale = await _client.GetFromJsonAsync<GetSaleByIdResult>($"/api/sales/{saleId}");
        //    sale!.TotalSaleAmount.Should().Be(400m); // 10 × 50 × 0.8
        //}

        //[Fact(DisplayName = "Should reject sale when item quantity exceeds 20")]
        //public async Task CreateSale_WithExcessiveQuantity_ShouldReturnBadRequest()
        //{
        //    var request = new CreateSaleRequest
        //    {
        //        Items = new List<SaleItemDto>
        //    {
        //        new() { ProductId = Guid.NewGuid(), ProductName = "Produto C", Quantity = 25, UnitPrice = 30m }
        //    }
        //    };

        //    var response = await _client.PostAsJsonAsync("/api/sales", request);
        //    response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        //}

        //[Fact(DisplayName = "Should cancel sale when valid ID is provided")]
        //public async Task CancelSale_WithValidId_ShouldReturnSuccess()
        //{
        //    var saleId = await CreateValidSaleAsync();

        //    var response = await _client.DeleteAsync($"/api/sales/{saleId}");
        //    response.StatusCode.Should().Be(HttpStatusCode.OK);

        //    var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
        //    result!.Success.Should().BeTrue();

        //    var sale = await _client.GetFromJsonAsync<GetSaleByIdResult>($"/api/sales/{saleId}");
        //    sale!.Cancelled.Should().BeTrue();
        //}

        //[Fact(DisplayName = "Should update sale and return updated ID")]
        //public async Task UpdateSale_WithValidData_ShouldReturnUpdatedId()
        //{
        //    var saleId = await CreateValidSaleAsync();

        //    var updateRequest = new UpdateSaleRequest
        //    {
        //        Id = saleId,
        //        Cancelled = false,
        //        UserId = Guid.NewGuid(),
        //        UserName = "updated_user",
        //        BranchId = Guid.NewGuid(),
        //        BranchName = "Nova Loja",
        //        BranchFullAddress = "Av. Atualizada, 456",
        //        Items = new List<SaleItemDto>()
        //    };

        //    var response = await _client.PatchAsJsonAsync("/api/sales", updateRequest);
        //    response.StatusCode.Should().Be(HttpStatusCode.OK);

        //    var updatedId = await response.Content.ReadFromJsonAsync<Guid>();
        //    updatedId.Should().Be(saleId);
        //}

        //[Fact(DisplayName = "Should retrieve sale by ID")]
        //public async Task GetSaleById_WithValidId_ShouldReturnSale()
        //{
        //    var saleId = await CreateValidSaleAsync();

        //    var response = await _client.GetAsync($"/api/sales/{saleId}");
        //    response.StatusCode.Should().Be(HttpStatusCode.OK);

        //    var sale = await response.Content.ReadFromJsonAsync<GetSaleByIdResult>();
        //    sale!.Id.Should().Be(saleId);
        //    sale.UserName.Should().NotBeNullOrEmpty();
        //}

        //[Fact(DisplayName = "Should return paginated sales list")]
        //public async Task GetPaginatedSales_ShouldReturnPagedResult()
        //{
        //    await CreateValidSaleAsync();
        //    await CreateValidSaleAsync();

        //    var response = await _client.PostAsJsonAsync("/api/sales?pageNumber=1&pageSize=10", new { });
        //    response.StatusCode.Should().Be(HttpStatusCode.OK);

        //    var result = await response.Content.ReadFromJsonAsync<PaginatedResponse<GetPaginatedSaleResponse>>();
        //    result!.Data.Should().NotBeEmpty();
        //    result.CurrentPage.Should().Be(1);
        //}

        //private async Task<Guid> CreateValidSaleAsync()
        //{
        //    var request = new CreateSaleRequest
        //    {
        //        UserId = Guid.NewGuid(),
        //        UserName = "vinicius",
        //        BranchId = Guid.NewGuid(),
        //        BranchName = "Loja Central",
        //        BranchFullAddress = "Rua das Laranjeiras, 123",
        //        Items = new List<SaleItemDto>
        //    {
        //        new() { ProductId = Guid.NewGuid(), ProductName = "Produto A", Quantity = 5, UnitPrice = 100m }
        //    }
        //    };

        //    var response = await _client.PostAsJsonAsync("/api/sales", request);
        //    var result = await response.Content.ReadFromJsonAsync<ApiResponseWithData<Guid>>();
        //    return result!.Data;
        //}
    }
}
