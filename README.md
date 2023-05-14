# RegulationHashCalculator
Use this program to recalculate Armored Core 4's regulation.bin file's hash.  

Simply drag and drop regulation.bin onto the exe.  
You do not need to use the correct raw format program for regulation.bin.  
This program will handle that.
All regulation archives inside however, you will need to correct that.  

Get that from here:
[AC4RawFormatCorrector][0]

# Technical Details
Armored Core 4 has a file entry in its regulation.bin BND3 file called md5.fmg.  
Despite it being named md5.fmg, it is not a FromSoftware FMG file as far as I can tell.  
It simply has a md5 hash of the regulation.bin file and the length of the entire file.  

The BND3 file is first made with 20 null bytes as the md5.fmg file entry,  
So that it can be interpreted correctly by software making BND3.  
Then after it is packed, the last 20 bytes are removed,  
And an md5 hash is made of the entire BND3.  

Then after the hash is made, it is added as the first 16 bytes at the end.  
Then the last 4 bytes are for the entire file size in bytes, including these last 4 bytes.  
The md5.fmg bytes, or the hash and length, are not written compressed.  
The md5 hash is written in Little Endian order as far as I can tell.  
The rest of the file, including the length included with the hash, is in Big Endian.

The reason raw format needs to be corrected is because  
Figuring out what byte order it should be written in is problematic for SoulsFormats and Yabber.  

[0]: https://github.com/WarpZephyr/AC4RawFormatCorrector/