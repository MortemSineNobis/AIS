using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Text;

namespace CAIS
{
    public class Message
    {
        public AISType MessageType { get; set; }
        public string AISMessage { get; set; }
        public NMEA NMEA { get; set; }
        public Decoded Decoded { get; set; }
        public Message()
        {
        }
        public Message(string AisMessage)
        {
            AISMessage = AisMessage;
            ValidateMessage(AISMessage);
            NMEA = new NMEA(AisMessage);
            MessageType = (AISType)NMEA.ais_id;
        }
        //public static Message FromJSON(string json)
        //{
        //    Message res = JSONDecode.GetMessage(json);
        //    return res;
        //}

        public void AddMessage(Message m)
        {
            NMEA.raw += m.NMEA.raw;
            NMEA.payload += m.NMEA.payload;
            NMEA.bit_array += m.NMEA.bit_array;
            AISMessage += m.AISMessage;

        }

        private static void ValidateMessage(string msg)
        {
            var values = msg.Split(',');
            if (values.Length != 7)
            {
                ////var resourceManager = new ResourceManager("FULLY_QUALIFIED_NAME_OF_RESOURCE_FILE", Assembly.GetExecutingAssembly());
                throw new InvalidNMEAMessageException("A NMEA message needs to have exactly 7 comma separated entries.", msg);
            }

            if (string.IsNullOrEmpty(values[0]))
            {
                //var resourceManager = new ResourceManager("FULLY_QUALIFIED_NAME_OF_RESOURCE_FILE", Assembly.GetExecutingAssembly());
                throw new InvalidNMEAMessageException("The NMEA message type is empty!", msg);
            }

            if (string.IsNullOrEmpty(values[1]))
            {
                //var resourceManager = new ResourceManager("FULLY_QUALIFIED_NAME_OF_RESOURCE_FILE", Assembly.GetExecutingAssembly());
                throw new InvalidNMEAMessageException("Number of sentences is empty!", msg);
            }

            if (string.IsNullOrEmpty(values[2]))
            {
                //var resourceManager = new ResourceManager("FULLY_QUALIFIED_NAME_OF_RESOURCE_FILE", Assembly.GetExecutingAssembly());
                throw new InvalidNMEAMessageException("Sentence number is empty!", msg);
            }

            if (string.IsNullOrEmpty(values[5]))
            {
                //var resourceManager = new ResourceManager("FULLY_QUALIFIED_NAME_OF_RESOURCE_FILE", Assembly.GetExecutingAssembly());
                throw new InvalidNMEAMessageException("The NMEA message body (payload) is empty.", msg);
            }

            if (string.IsNullOrEmpty(values[6]))
            {
                //var resourceManager = new ResourceManager("FULLY_QUALIFIED_NAME_OF_RESOURCE_FILE", Assembly.GetExecutingAssembly());
                throw new InvalidNMEAMessageException("NMEA checksum (NMEA 0183 Standard CRC16) is empty.", msg);
            }

            try
            {
                var sentence_num = Convert.ToInt32(values[1]);
                if (sentence_num > 9)
                {
                    //var resourceManager = new ResourceManager("FULLY_QUALIFIED_NAME_OF_RESOURCE_FILE", Assembly.GetExecutingAssembly());
                    throw new InvalidNMEAMessageException("Number of sentences exceeds limit of 9 total sentences.", msg);
                }
            }
            catch (FormatException)
            {
                //var resourceManager = new ResourceManager("FULLY_QUALIFIED_NAME_OF_RESOURCE_FILE", Assembly.GetExecutingAssembly());
                throw new InvalidNMEAMessageException("Invalid sentence number. No Number.", msg);
            }

            try
            {
                var sentence_num = Convert.ToInt32(values[2]);
                if (sentence_num > 9)
                {
                    //var resourceManager = new ResourceManager("FULLY_QUALIFIED_NAME_OF_RESOURCE_FILE", Assembly.GetExecutingAssembly());
                    throw new InvalidNMEAMessageException("Sentence number exceeds limit of 9 total sentences.", msg);
                }
            }
            catch (FormatException)
            {
                //var resourceManager = new ResourceManager("FULLY_QUALIFIED_NAME_OF_RESOURCE_FILE", Assembly.GetExecutingAssembly());
                throw new InvalidNMEAMessageException("Invalid sentence number. No Number.", msg);
            }

            if (!string.IsNullOrEmpty(values[3]))
                try
                {
                    var sentence_num = Convert.ToInt32(values[3]);
                    if (sentence_num > 9)
                    {
                        //var resourceManager = new ResourceManager("FULLY_QUALIFIED_NAME_OF_RESOURCE_FILE", Assembly.GetExecutingAssembly());
                        throw new InvalidNMEAMessageException("Number of sequential message ID exceeds limit of 9 total sentences.", msg);
                    }
                }
                catch (FormatException)
                {
                    //var resourceManager = new ResourceManager("FULLY_QUALIFIED_NAME_OF_RESOURCE_FILE", Assembly.GetExecutingAssembly());
                    throw new InvalidNMEAMessageException("Invalid sequential message ID. No Number.", msg);
                }
            if (values[5].Length > 82)
            {
                //var resourceManager = new ResourceManager("FULLY_QUALIFIED_NAME_OF_RESOURCE_FILE", Assembly.GetExecutingAssembly());
                throw new InvalidNMEAMessageException($"{msg} has more than 82 characters of payload.", msg);
            }

            if (values[0][0] != 0x21)
            {
                //var resourceManager = new ResourceManager("FULLY_QUALIFIED_NAME_OF_RESOURCE_FILE", Assembly.GetExecutingAssembly());
                throw new InvalidNMEAMessageException(
                    $"'NMEAMessage' only supports !AIVDM/!AIVDO encapsulated messages. " +
                    $"These start with an '!', but got '{values[0][0]}'"
                    , msg);
            }
        }
    }
}
