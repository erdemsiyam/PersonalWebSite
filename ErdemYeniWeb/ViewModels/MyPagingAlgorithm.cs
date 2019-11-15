using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErdemYeniWeb.ViewModels
{
    public class MyPagingAlgorithm
    {
        public int itemCount { get; set; }
        public int selectPage { get; set; }
        public int totalPage { get; set; }
        public int minPage { get; set; }

        public void applyMax5Paging(int totalPageCount,int selectedPage)
        {
            totalPage = totalPageCount;
            // page count sizing. only 5 length. (My Algorithm)
            minPage = 1;
            selectPage = selectedPage;
            if (totalPage > 5)
            {
                if (selectPage > 3)
                {
                    if (totalPage - selectPage >= 2)
                    {
                        totalPage = selectPage + 2;
                        minPage = selectPage - 2;
                    }
                    else if (totalPage - selectPage < 2)
                    {
                        minPage = selectPage - 2;
                        minPage -= 2 + (selectPage - totalPage);
                    }
                }
                else if (selectPage <= 3)
                {
                    totalPage = 5;
                }
            }
        }
    }
}