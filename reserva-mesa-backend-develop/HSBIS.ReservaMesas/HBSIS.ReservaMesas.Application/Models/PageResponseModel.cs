using System.Collections.Generic;

namespace HBSIS.ReservaMesas.Application.Models
{
    public class PageResponseModel<T>
    {
        public int CurrentPage { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; } = 14;
        public IEnumerable<T> Data { get; set; }

        public PageResponseModel(int currentPage, int count, IEnumerable<T> data)
        {
            this.CurrentPage = currentPage;
            this.Count = count;
            this.Data = data;
        }
    }
}
