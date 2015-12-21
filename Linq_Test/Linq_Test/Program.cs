using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_Test
{
    class Program
    {


        static void Main(string[] args)
        {
            long l = 1099511627774;
            string str = string.Format("{0:X}", l);


            //Group
            {

                List<ItemDefine> FullList = new List<ItemDefine>()
                {
                    new ItemDefine(){SN="0001", DownloadStatus = DownloadStatusEnum.Canceled},
                    new ItemDefine(){SN="0002", DownloadStatus = DownloadStatusEnum.Canceled},
                    new ItemDefine(){SN="0003", DownloadStatus = DownloadStatusEnum.Canceled},
                    new ItemDefine(){SN="0004", DownloadStatus = DownloadStatusEnum.Canceled},
                    new ItemDefine(){SN="0005", DownloadStatus = DownloadStatusEnum.Canceled},

                    new ItemDefine(){SN="0004", DownloadStatus = DownloadStatusEnum.AvailableForDownloaded},
                    new ItemDefine(){SN="0005", DownloadStatus = DownloadStatusEnum.AvailableForDownloaded},
                    new ItemDefine(){SN="0006", DownloadStatus = DownloadStatusEnum.AvailableForDownloaded},
                    new ItemDefine(){SN="0007", DownloadStatus = DownloadStatusEnum.AvailableForDownloaded},

                };


                IEnumerable<IGrouping<string, ItemDefine>> List_Group = FullList.GroupBy(item => item.SN);

                List<ItemDefine> DuplicatedList = new List<ItemDefine>();
                IEnumerable<ItemDefine> resultList = null;
                foreach (var group in List_Group)
                {
                    if (group.Count() > 1)
                    {
                        var tempList2 = group.Where(item => item.DownloadStatus != DownloadStatusEnum.AvailableForDownloaded);
                        DuplicatedList.AddRange(tempList2);
                    }
                }

                resultList = FullList.Except(DuplicatedList).ToList();


            }

        }
    }


    enum DownloadStatusEnum
    {
        AvailableForDownloaded,
        Canceled,
    }
    class ItemDefine
    {
        public string SN { get; set; }

        public DownloadStatusEnum DownloadStatus { get; set; }
    }
}
