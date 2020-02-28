using System;
using System.IO;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace FactorioSync
{
    class ZacFunctions
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private enum ErrorLvl
        {
            Debug,
            Error
        }

        private static void LogString(string text, ListBox lstBox = null, ErrorLvl error = ErrorLvl.Debug, string color = "")
        {
            if (lstBox != null)
            {
                var defaultBrushColor = Brushes.Blue;
                var successBrushColor = Brushes.Green;
                var errorBrushColor = Brushes.Red;
                var warningBrushColor = Brushes.Orange;

                var currentBrushe = defaultBrushColor;

                if (color == "success")
                {
                    currentBrushe = successBrushColor;
                } 
                else if (color == "error")
                {
                    currentBrushe = errorBrushColor;
                } 
                else if (color == "warning")
                {
                    currentBrushe = warningBrushColor;
                }

                lstBox.Items.Add(new ListBoxItem {
                    Content = String.Format(text),
                    Foreground = currentBrushe
                });
            }
            else
            {
                if (error == ErrorLvl.Debug) 
                {
                    logger.Debug(text);
                }
                else if (error == ErrorLvl.Error)
                {
                    logger.Error(text);
                }
                else
                {
                    logger.Info(text);
                }
                    
            }
        }

        public static void CopyFile(string srcFolder, string destFolder, ListBox lstBox = null)
        {
            string[] originalFiles = Directory.GetFiles(srcFolder, "*", SearchOption.AllDirectories);

            LogString(String.Format("Début : {0}", DateTime.Now), lstBox);
            LogString(String.Format("Il y a {0} fichiers dans {1}", originalFiles.Length, srcFolder), lstBox);

            Array.ForEach(originalFiles, (originalFileLocation) =>
            {
                try
                {
                    FileInfo originalFile = new FileInfo(originalFileLocation);
                    FileInfo destFile = new FileInfo(originalFileLocation.Replace(srcFolder, destFolder));

                    LogString(String.Format("Copie du fichier {0} vers le répertoire {1}", originalFile, destFolder), lstBox);

                    if (destFile.Exists)
                    {
                        LogString(String.Format("Le fichier {0} existe déja !", destFile), lstBox);
                        if (originalFile.Length > destFile.Length)
                        {
                            LogString(String.Format("Le fichier d'origine \"{0}\" est plus récent que le fichier de destination \"{1}\" : On l'écrase", originalFile, destFile), lstBox, ErrorLvl.Debug, "succes");
                            originalFile.CopyTo(destFile.FullName, true);
                        }
                    }
                    else
                    {
                        LogString(String.Format("Création/Sélection du répertoire \"{0}\"", destFile.DirectoryName), lstBox, ErrorLvl.Debug, "warning");
                        Directory.CreateDirectory(destFile.DirectoryName);
                        LogString(String.Format("Copie du fichier {0}", destFile.FullName), lstBox, ErrorLvl.Debug, "success");
                        originalFile.CopyTo(destFile.FullName, false);
                    }
                }
                catch (Exception e)
                {
                    LogString(e.ToString(), lstBox, ErrorLvl.Error, "error");
                }
            });

            LogString(String.Format("Fin : {0}", DateTime.Now), lstBox);
            LogString("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*", lstBox);
        }
    }
}
