using Ambev.DeveloperEvaluation.Integration.Common;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.Support.Domain.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Repositories
{
    public class UserRepositoryTests
    {
        private readonly DefaultContext _context;
        private readonly UserRepository _repository;

        public UserRepositoryTests()
        {
            _context = MockingData.CreateInMemoryContext();
            _repository = new UserRepository(_context);
        }

        /// <summary>
        /// Tests that a user is correctly persisted to the database when created.
        /// </summary>
        [Fact(DisplayName = "Should persist user when created")]
        public async Task Given_ValidUser_When_Created_Then_ShouldBePersisted()
        {
            // Arrange
            var user = UserTestData.GenerateValidUser();

            // Act
            var result = await _repository.CreateAsync(user);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();

            var dbUser = await _context.Users.FindAsync(result.Id);
            dbUser.Should().NotBeNull();
            dbUser.Email.Should().Be(user.Email);
        }

        /// <summary>
        /// Tests that a user can be retrieved by their unique identifier.
        /// </summary>
        [Fact(DisplayName = "Should retrieve user by ID")]
        public async Task Given_UserExists_When_GetByIdCalled_Then_ShouldReturnUser()
        {
            // Arrange
            var user = UserTestData.GenerateValidUser();
            await _repository.CreateAsync(user);

            // Act
            var result = await _repository.GetByIdAsync(user.Id);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(user.Id);
        }

        /// <summary>
        /// Tests that a user can be retrieved by their email address.
        /// </summary>
        [Fact(DisplayName = "Should retrieve user by email")]
        public async Task Given_UserExists_When_GetByEmailCalled_Then_ShouldReturnUser()
        {
            // Arrange
            var user = UserTestData.GenerateValidUser();
            await _repository.CreateAsync(user);

            // Act
            var result = await _repository.GetByEmailAsync(user.Email);

            // Assert
            result.Should().NotBeNull();
            result!.Email.Should().Be(user.Email);
        }

        /// <summary>
        /// Tests that deleting a user removes them from the database and returns true.
        /// </summary>
        [Fact(DisplayName = "Should remove user when deleted")]
        public async Task Given_UserExists_When_Deleted_Then_ShouldBeRemoved()
        {
            // Arrange
            var user = UserTestData.GenerateValidUser();
            await _repository.CreateAsync(user);

            // Act
            var deleted = await _repository.DeleteAsync(user.Id);

            // Assert
            deleted.Should().BeTrue();
            var check = await _repository.GetByIdAsync(user.Id);
            check.Should().BeNull();
        }

        /// <summary>
        /// Tests that creating a user with a cancelled token throws an OperationCanceledException.
        /// </summary>
        [Fact(DisplayName = "Should throw OperationCanceledException when token is cancelled")]
        public async Task Given_CancellationToken_When_CreateAsyncCalled_Then_ShouldThrowException()
        {
            // Arrange
            var user = UserTestData.GenerateValidUser();
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            // Act
            Func<Task> act = async () => await _repository.CreateAsync(user, cts.Token);

            // Assert
            await act.Should().ThrowAsync<OperationCanceledException>();

        }
    }
}
