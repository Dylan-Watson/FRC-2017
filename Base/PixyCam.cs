using System.Collections.Generic;
using WPILib;

namespace Base
{
    public class PixyCam
    {
        private const int PIXY_I2_C_DEFAULT_ADDR = 0x54;
        private const int PIXY_INITIAL_ARRAYSIZE = 30;
        private const int PIXY_MAXIMUM_ARRAYSIZE = 130;
        private const int PIXY_START_WORD = 0xaa55;
        private const int PIXY_START_WORD_CC = 0xaa56;
        private const int PIXY_START_WORDX = 0x55aa;
        private const int PIXY_MAX_SIGNATURE = 7;
        private const int PIXY_DEFAULT_ARGVAL = 0xffff;
        private const long PIXY_MIN_X = 0L;
        private const long PIXY_MAX_X = 319L;
        private const long PIXY_MIN_Y = 0L;
        private const long PIXY_MAX_Y = 199L;
        private const long PIXY_RCS_MIN_POS = 0L;
        private const long PIXY_RCS_MAX_POS = 1000L;
        private const long PIXY_RCS_CENTER_POS = ((PIXY_RCS_MAX_POS - PIXY_RCS_MIN_POS)/2);

        private enum BlockType
        {
            NormalBlock, //normal color recognition
            CcBlock //color-code(chnage in angle) recognition
        };

        private struct Block
        {
            short signature; //Identification number for your object - you could set it in the pixymon
            short x; //0 - 320
            short y; //0 - 200
            short width;
            short height;
            short angle;
        }

        private BlockType blockType;
        bool skipStart;
        private I2C i2C;
        private List<Block> blocks = new List<Block>(100);

        public PixyCam()
        {
            i2C = new I2C(I2C.Port.Onboard, PIXY_I2_C_DEFAULT_ADDR);
        }

        public uint getWord() //Getting two Bytes from Pixy (The full information)
        {
            var buffer = new byte[2];
            buffer[0] = 0;
            buffer[1] = 0;

            i2C.ReadOnly(buffer, 2);
            return (uint) ((buffer[1] << 8) | buffer[0]);
            //shift buffer[1] by 8 bits and add( | is bitwise or) buffer[0] to it
        }

        public uint getByte() //gets a byte
        {
            var buffer = new byte[1];
            buffer[0] = 0;

            i2C.ReadOnly(buffer, 1);
            return buffer[0];
        }

        public bool getStart() //checks whether if it is start of the normal frame, CC frame, or the data is out of sync
        {
            uint w, lastw;

            lastw = 0xffff;

            while (true)
            {
                w = getWord(); //This it the function right underneath
                if (w == 0 && lastw == 0)
                {
                    //delayMicroseconds(10);
                    return false;
                }
                else if (w == PIXY_START_WORD && lastw == PIXY_START_WORD)
                {
                    blockType = BlockType.NormalBlock;
                    return true;
                }
                else if (w == PIXY_START_WORD_CC && lastw == PIXY_START_WORD)
                {
                    blockType = BlockType.CcBlock;
                    return true;
                }
                else if (w == PIXY_START_WORDX)
                    //when byte recieved was 0x55aa instead of otherway around, the code syncs the byte
                {
                    Report.General("Pixy: reorder");
                    getByte(); // resync
                }
                lastw = w;
            }
        }
    }

}

