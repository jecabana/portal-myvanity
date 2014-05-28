using System.IO;

namespace MyVanity.Domain
{
    public class Helpers
    {
        public static ContentType? ResolveContentTypeFromName(string fileName)
        {
            var extension = Path.GetExtension(fileName);

            if (extension == null) return null;

            var ext = extension.ToLower();
            switch (ext)
            {
                case ".docx":
                case ".doc":
                    return ContentType.Word;
                case ".ogg":
                case ".mp4":
                    return ContentType.Video;
                case ".pdf":
                    return ContentType.Pdf;
                case ".jpeg":
                case ".jpg":
                case ".png":
                    return ContentType.Photo;
            }

            return null;
        }
    }
}
