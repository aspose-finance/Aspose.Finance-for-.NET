using Tools.Foundation.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Foundation.Handler.Output
{
    public interface IOutputDataHandler
    {
        //The methods which should be implemented in the descendant class
        List<OutputFileDescription> getFileDescription(FileDescription fileDesc);
        List<Stream> getFile(FileDescription fileDesc);
        List<OutputFileDescription> saveFile(Stream st, InputFileDescription inputFileDesc);
        bool removeFile(OutputFileDescription outputFileDesc);
    }
}
