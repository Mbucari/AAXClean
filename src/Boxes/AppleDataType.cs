using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAXClean.Boxes
{
    public enum AppleDataType : uint
    {
        /// <summary>
        ///    The box contains UTF-8 text.
        /// </summary>
        ContainsText = 0x01,

        /// <summary>
        ///    The box contains binary data.
        /// </summary>
        ContainsData = 0x00,

        /// <summary>
        ///    The box contains data for a tempo box.
        /// </summary>
        ForTempo = 0x15,

        /// <summary>
        ///    The box contains a raw JPEG image.
        /// </summary>
        ContainsJpegData = 0x0D,

        /// <summary>
        ///    The box contains a raw PNG image.
        /// </summary>
        ContainsPngData = 0x0E,

        /// <summary>
        ///    The box contains a raw BMP image.
        /// </summary>
        ContainsBmpData = 0x1B

    }
}
