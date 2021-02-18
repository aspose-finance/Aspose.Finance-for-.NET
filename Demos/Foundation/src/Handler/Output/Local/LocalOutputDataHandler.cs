using Tools.Foundation.Config;
using Tools.Foundation.Handler;
using Tools.Foundation.Handler.Exceptions;
using Tools.Foundation.Handler.Exceptions.Local;
using Tools.Foundation.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Tools.Foundation.Handler.Output.Local
{
    public class LocalOutputDataHandler : IOutputDataHandler
    {
        private string locale;
        public static Configs Configs = new Configs();
        // Instantiate Tools.Foundation.Handler.Exceptions.IExceptionHandler class object to handle the exceptions
        IExceptionHandler exceptionHandler = null;

        public LocalOutputDataHandler()
        {
            new LocalOutputDataHandler(locale, null);
        }

        public LocalOutputDataHandler(string locale, IExceptionHandler exceptionHandler)
        {
            if (!String.IsNullOrEmpty(locale))
            {
                this.locale = locale;
            }
            //Check if the outputHandler was set
            if (exceptionHandler == null)
            {
                //Use default
                this.exceptionHandler = new LocalExceptionHandler();
            }
            else
            {
                //Use custom
                this.exceptionHandler = exceptionHandler;
            }
        }

        /// <summary>
        ///Save file localy from stream
        /// </summary>
        /// <param name="Stream st">File stream</param>
        /// <param name="InputFileDescription inputFileDesc">InputFileDescription inputFileDesc</param>
        /// <returns> List[OutputFileDescription]</returns>
        public List<OutputFileDescription> saveFile(Stream st, InputFileDescription inputFileDesc)
        {
            //Result ibject
            List<OutputFileDescription> result = new List<OutputFileDescription>();
            try
            {
                if (st != null)
                {
                    if (!String.IsNullOrEmpty(inputFileDesc.OperationId) && !String.IsNullOrEmpty(inputFileDesc.Name))
                    {
                        try
                        {
                            string folder = getDirectory(inputFileDesc.OperationId) + "/";
                            //Create new FileStream object which will be saved localy
                            string pathForFile = String.Concat(folder, inputFileDesc.Name);
                            using (System.IO.FileStream output = new System.IO.FileStream(pathForFile, FileMode.Create, FileAccess.Write))
                            {
                                //Save stream to the local file
                                st.CopyTo(output);
                                string[] fileInfo = Directory.GetFiles(folder);
                                //Set result data
                                foreach (string file in fileInfo)
                                {
                                    FileInfo fileInfoTogetSize = new FileInfo(file);
                                    OutputFileDescription fileDesc = new OutputFileDescription();
                                    fileDesc.CreationTime = DateTime.UtcNow;
                                    fileDesc.Name = Path.GetFileName(pathForFile);
                                    fileDesc.Path = folder;
                                    fileDesc.Size = fileInfoTogetSize.Length;
                                    result.Add(fileDesc);
                                    fileInfoTogetSize = null;
                                }
                            }
                            st.Close();
                        }
                        catch (DocumentsToolsException)
                        {
                            throw new GdtFileNotFoundException(locale, this.exceptionHandler);
                        }
                    }
                    else
                    {
                        throw new GdtFileNotFoundException(locale, this.exceptionHandler);
                    }
                }
                else
                {
                    throw new GdtNullReferenceException(locale, this.exceptionHandler);
                }
            }
            catch (DocumentsToolsException ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        ///Create a directory for the input files
        /// </summary>
        /// <param name="string operationId">Id of the opperation - used as a sub folder name</param>
        /// <returns>string</returns>
        private string getDirectory(string operationId)
        {
            try
            {
                //Get configuration options
                string configurationFilePath = new InternalConfigurations().configurationFilePath;
                string configPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                new ConfigLoader(configPath + configurationFilePath).LoadConfigs(Configs);
                string path = String.Concat(Configs.outputdirectory, "/" + operationId + "/", Configs.outputdirname);
                //Create sub folder original in the temppath folder
                DirectoryInfo di = new DirectoryInfo(path);
                try
                {
                    di.Create();
                    return di.FullName;
                }
                catch (DocumentsToolsException)
                {
                    throw new GdtFileNotFoundException(locale, this.exceptionHandler);
                }
            }
            catch (DocumentsToolsException)
            {
                throw new GdtNullReferenceException(locale, this.exceptionHandler);
            }

        }

        /// <summary>
        ///Get file description
        /// </summary>
        /// <param name="FileDescription fileDesc">FileDescription of the </param>
        /// <returns> List[OutputFileDescription]</returns>
        public List<OutputFileDescription> getFileDescription(FileDescription fileDesc)
        {
            //Initialaze the InputFileDescription obhect wich will be returned
            List<OutputFileDescription> result = new List<OutputFileDescription>();
            //Check if fileid is empty
            if (fileDesc != null)
            {
                if (!String.IsNullOrEmpty(fileDesc.OperationId))
                {
                    try
                    {
                        string operationId = fileDesc.OperationId;
                        //Get configuration options

                        string path = getDirectory(operationId);
                        string[] fileInfo = Directory.GetFiles(path);
                        if (fileInfo.Count() == 0)
                        {
                            OutputFileDescription fileDescription = new OutputFileDescription();
                            fileDescription.Path = path;
                            result.Add(fileDescription);
                        }
                        else
                        {
                            //Get FileInfo for the fileId
                            foreach (string file in fileInfo)
                            {
                                FileInfo fi = new FileInfo(file);
                                //Set result data
                                OutputFileDescription fileDescription = new OutputFileDescription();
                                fileDescription.Name = fi.Name;
                                fileDescription.Path = path;
                                fileDescription.Size = fi.Length;
                                fileDescription.CreationTime = fi.CreationTimeUtc;
                                result.Add(fileDescription);
                            }
                        }
                    }
                    catch (DocumentsToolsException)
                    {
                        throw new GdtFileNotFoundException(locale, this.exceptionHandler);
                    }
                }
                else
                {
                    throw new GdtNullReferenceException(locale, this.exceptionHandler);
                }
            }
            else
            {
                throw new GdtNullReferenceException(locale, this.exceptionHandler);
            }
            return result;
        }

        /// <summary>
        ///Get local file stream
        /// </summary>
        /// <param name="FileDescription fileDesc">FileDescription object which contains the file data</param>
        /// <returns> List[Stream]</returns>
        public List<Stream> getFile(FileDescription fileDesc)
        {
            //Initialize result stream
            List<Stream> st = new List<Stream>();
            //Check if FileDescription is null
            if (fileDesc != null)
            {
                try
                {
                    string operationId = fileDesc.OperationId;
                    //Get configuration options
                    string configurationFilePath = new InternalConfigurations().configurationFilePath;
                    string configPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                    new ConfigLoader(configPath + configurationFilePath).LoadConfigs(Configs);
                    string path = String.Concat(Configs.outputdirectory, "/" + operationId + "/", Configs.outputdirname);
                    string[] fileInfo = Directory.GetFiles(path);
                    //Get FileInfo for the fileId
                    foreach (string file in fileInfo)
                    {
                        //Get stream of the file
                        st.Add(new FileStream(file, FileMode.Open));
                    }

                }
                catch (DocumentsToolsException)
                {
                    throw new GdtFileNotFoundException(locale, this.exceptionHandler);
                }
            }
            else
            {
                throw new GdtNullReferenceException(locale, this.exceptionHandler);
            }
            return st;
        }

        /// <summary>
        ///Delete local file
        /// </summary>
        /// <param name="InputFileDescription fileDesc">FileDescription object which contains the file data</param>
        /// <returns>bool</returns>
        public bool removeFile(OutputFileDescription outputFileDesc)
        {
            //Initialize the result variable with the default value
            bool result = false;
            if (outputFileDesc != null)
            {
                try
                {
                    if (!String.IsNullOrEmpty(outputFileDesc.Path))
                    {
                        //Delete the file
                        File.Delete(outputFileDesc.Path);
                        result = true;
                    }
                    else
                    {
                        throw new DocumentsToolsException();
                    }
                }
                catch (DocumentsToolsException)
                {
                    throw new GdtFileNotFoundException(locale, this.exceptionHandler);
                }
            }
            else
            {
                throw new GdtNullReferenceException(locale, this.exceptionHandler);
            }
            return result;
        }
    }
}