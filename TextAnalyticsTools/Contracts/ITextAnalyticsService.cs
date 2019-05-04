using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ImageServe.Models;

namespace TextAnalyticsTools.Contracts
{
    public interface ITextAnalyticsService
    {
        Task<ICollection<ImageTag>> GenerateTagsAsync(string text);
    }
}
