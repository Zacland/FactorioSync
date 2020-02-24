using System;
using System.IO;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioSync
{
    class ZacFunctions
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void CopyFile(string Src_FOLDER, string Dest_FOLDER)
        {
            string[] originalFiles = Directory.GetFiles(Src_FOLDER, "*", SearchOption.AllDirectories);

            logger.Debug("Début : {}", DateTime.Now);
            logger.Debug("Il y a {} fichiers dans {}", originalFiles.Length, Src_FOLDER);

            Array.ForEach(originalFiles, (originalFileLocation) =>
            {
                try
                {
                    FileInfo originalFile = new FileInfo(originalFileLocation);
                    FileInfo destFile = new FileInfo(originalFileLocation.Replace(Src_FOLDER, Dest_FOLDER));

                    logger.Debug("Copie du fichier {} vers le répertoire {}", originalFile, Dest_FOLDER);

                    if (destFile.Exists)
                    {
                        logger.Debug("Le fichier {} existe déja !", destFile);
                        if (originalFile.Length > destFile.Length)
                        {
                            logger.Debug("Le fichier d'origine \"{}\" est plus récent que le fichier de destination \"{}\" : On l'écrase", originalFile, destFile);
                            originalFile.CopyTo(destFile.FullName, true);
                        }
                    }
                    else
                    {
                        logger.Debug("Création/Sélection du répertoire \"{}\"", destFile.DirectoryName);
                        Directory.CreateDirectory(destFile.DirectoryName);
                        logger.Debug("Copie du fichier {}", destFile.FullName);
                        originalFile.CopyTo(destFile.FullName, false);
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e);
                }
            });

            logger.Debug("Fin : {}", DateTime.Now);
            logger.Debug("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
        }
    }
}
