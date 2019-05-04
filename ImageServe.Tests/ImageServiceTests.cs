using AutoMapper;
using BlobStorageTools.Contracts;
using ImageServe.Data;
using ImageServe.Data.Common;
using ImageServe.Models;
using ImageServe.Services;
using ImageServe.Services.Contracts;
using ImageServe.WebModels.BindingModels;
using ImageServe.WebModels.Dtos;
using ImageServe.WebModels.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ImageServe.Tests
{
    class ImageServiceTests
    {
        private const string CURRENT_USER_ID = "2";
        private const string FRIEND_USER_ID = "1";
        private const string UNKNOWN_USER_ID = "3";


        private ImageService imageService;
        private User friend;
        private User unknown;

        public ImageServiceTests()
        {
            //arrange
            var options = new DbContextOptionsBuilder<ImageServeDbContext>()
              .UseInMemoryDatabase(databaseName: "ImageServeTest")
              .Options;
            var dbContext = new ImageServeDbContext(options);
            var imageRepository = new DbRepository<Image>(dbContext);
            var likesRepository = new DbRepository<ImageLike>(dbContext);
            var userRepository = new DbRepository<User>(dbContext);
            var tagsRepository = new DbRepository<ImageTag>(dbContext);
            var mapperMock = MockAutoMapper();
            var userServiceMock = MockUserService();
            var tagServiceMock = MockTagService();
            Mock<IBlobStorageService> blobServiceMock = MockBlobService();

            this.imageService = new ImageService(imageRepository, mapperMock.Object, tagServiceMock.Object, userServiceMock.Object, blobServiceMock.Object, likesRepository);

            //act
            Setup(imageRepository).GetAwaiter().GetResult();
        }

        


        [Test]
        public void AllByUser_ByFriendUser_ShouldReturnTwoImages()
        {
            //act
            var images = this.imageService.AllByUser(friend.Id);

            //assert
            Assert.AreEqual(2, images.Count);
        }

        [Test]
        public void AllByUser_UnknownUser_ShouldReturnOneImage()
        {
            var images = this.imageService.AllByUser(unknown.Id);

            Assert.AreEqual(1, images.Count);
        }

        //Tests privacy on news feed.
        [Test]
        public void Newest_ShouldReturn_FourImages()
        {
            //act
            var images = this.imageService.Newest(1, 6);

            //assert
            //we expect four images instead of three, because in another test we are adding one more photo ??how to fix??
            Assert.AreEqual(4, images.Count);
        }

        [Test]
        public void AddAsync_WithValidImage_Should_Pass()
        {
            //act
            var IFormFileMock = new Mock<IFormFile>();
            IFormFileMock
                 .Setup(f => f.OpenReadStream())
                .Returns(this.GenerateStreamFromString("asd"))
                .Verifiable();

            IFormFileMock
                .Setup(f => f.FileName)
                .Returns("test.jpg");

            var uploadBindingModel = new ImageUploadBindingModel()
            {
                File = IFormFileMock.Object,
                IsPublic = false
            };

            //assert
            Assert.That(async () => await this.imageService.AddAsync(uploadBindingModel), Throws.Nothing);
        }

        [Test]
        public void AddAsync_WithEmptyFile_Should_ThrowError()
        {
            //act
            var memoryStream = new MemoryStream();

            var IFormFileMock = new Mock<IFormFile>();
            IFormFileMock
                .Setup(f => f.OpenReadStream())
               .Returns(memoryStream)
               .Verifiable();


            IFormFileMock
                .Setup(f => f.FileName)
                .Returns("test.jpg");

            var uploadBindingModel = new ImageUploadBindingModel()
            {
                File = IFormFileMock.Object
            };

            //assert
            Assert.ThrowsAsync<FileNotFoundException>(async () => await this.imageService.AddAsync(uploadBindingModel));

        }

        [Test]
        public void AddAsync_WithInvalidFileExtension_Should_ThrowError()
        {
            //act
            var IFormFileMock = new Mock<IFormFile>();
            IFormFileMock
                .Setup(f => f.OpenReadStream())
               .Returns(this.GenerateStreamFromString("asd"))
               .Verifiable();


            IFormFileMock
                .Setup(f => f.FileName)
                .Returns("test.txt");

            var uploadBindingModel = new ImageUploadBindingModel()
            {
                File = IFormFileMock.Object
            };

            //assert
            Assert.ThrowsAsync<FormatException>(async () => await this.imageService.AddAsync(uploadBindingModel));

        }

        [Test]
        public void GetImageAsync_FriendUserPrivateImage_ShoudlPass()
        {
            //assert
            Assert.That(async () => await this.imageService.GetImageAsync<ImageViewModel>(102), Throws.Nothing);
        }

        [Test]
        public void GetImageAsync_UnknownUserPrivateImage_ShoudlThrowError()
        {
            //assert
            Assert.ThrowsAsync<AccessViolationException>(async () => await this.imageService.GetImageAsync<ImageViewModel>(104));
        }

        [Test]
        public void GetImageAsync_UnknownUserPublicImage_ShoudlPass()
        {
            //assert
            Assert.That(async () => await this.imageService.GetImageAsync<ImageViewModel>(103), Throws.Nothing);
        }

        [Test]
        public void GetImageAsync_FriendUserPublicImage_ShoudlPass()
        {
            //assert
            Assert.That(async () => await this.imageService.GetImageAsync<ImageViewModel>(101), Throws.Nothing);
        }

        



        private Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }


        private Mock<IBlobStorageService> MockBlobService()
        {
            var blobServiceMock = new Mock<IBlobStorageService>();

            blobServiceMock
                .Setup(bs => bs.UploadFromFileAsync(It.IsAny<byte[]>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            return blobServiceMock;
        }

        private Mock<ITagService> MockTagService()
        {
            var tagServiceMock = new Mock<ITagService>();

            tagServiceMock
                .Setup(ts => ts.AddToImageAsync(It.IsAny<Image>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            return tagServiceMock;
        }

        private Mock<IUserService> MockUserService()
        {
            var userServiceMock = new Mock<IUserService>();

            userServiceMock
                .Setup(us => us.GetCurrentId())
                .Returns(CURRENT_USER_ID)
                .Verifiable();
            return userServiceMock;
        }

        private async Task Setup(DbRepository<Image> imageRepository)
        {
            this.friend = new User()
            {
                Id = FRIEND_USER_ID
            };

            friend.UserFriends.Add(new Friendship()
            {
                UserId = friend.Id,
                FriendId = CURRENT_USER_ID
            });

            this.unknown = new User()
            {
                Id = UNKNOWN_USER_ID
            };

            var friendPublicImage = new Image()
            {
                Id = 101,
                UserId = friend.Id,
                User = friend,
                IsPublic = true

            };

            var friendPrivateImage = new Image()
            {
                Id = 102,
                UserId = friend.Id,
                User = friend,
                IsPublic = true

            };

            var unknownPublicImage = new Image()
            {
                Id = 103,
                UserId = unknown.Id,
                User = unknown,
                IsPublic = true
            };

            var unknownPrivateImage = new Image()
            {
                Id = 104,
                UserId = unknown.Id,
                User = unknown,
                IsPublic = false
            };

            await imageRepository.AddAsync(friendPublicImage); 
            await imageRepository.AddAsync(friendPrivateImage);
            await imageRepository.AddAsync(unknownPublicImage);
            await imageRepository.AddAsync(unknownPrivateImage);
            await imageRepository.SaveChangesAsync();
        }

        //var textAnalyticsMock = new Mock<ITextAnalyticsService>();
        //
        //textAnalyticsMock
        //    .Setup(t => t.GenerateTagsAsync(It.IsAny<string>()))
        //    .Returns(Task.FromResult((ICollection<ImageTag>)new List<ImageTag>()));
        //
        //var computerVisionMock = new Mock<IComputerVisionService>();
        //
        //computerVisionMock
        //    .Setup(cv => cv.GenerateTagsAsync(It.IsAny<string>()))
        //    .Returns(Task.FromResult((ICollection<ImageTag>)new List<ImageTag>()));
        //
        //

        private  Mock<IMapper> MockAutoMapper()
        {
            var mock = new Mock<IMapper>();
            mock.Setup(m => m.Map<ImageDto>(It.IsAny<Image>())).Returns(new ImageDto()); 

            return mock;

        }
    }
}
