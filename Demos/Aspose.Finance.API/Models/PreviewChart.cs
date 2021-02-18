namespace Aspose.Finance.API.Models
{
    // 预览图表
    public class PreviewChart
    {
        // workbook hash
        public int WorkbookHash { get; set; }

        // sheet索引
        public int SheetIndex { get; set; }

        // 图表的哈希值
        public int ChartHash { get; set; }

        public string ChartName { get; set; }

        // 图表保存图片地址
        public string ImgFolderName { get; set; }

        public string ImgFileName { get; set; }
    }
}