
namespace ConsoleApp1 {

    public class TagOpeningHandler : ICharHandler
    {
        private XmlParser xmlParser;

        public TagOpeningHandler(XmlParser xmlParser) {
            this.xmlParser = xmlParser;
        }

        public int handle(int currentIndex)
        {
            bool nextCharExists = currentIndex + 1 < xmlParser.charArray.Length;
            if(nextCharExists && xmlParser.charArray[currentIndex + 1].Equals('/')) {
                if(xmlParser.currentState == XmlState.OpeningTagEnded || xmlParser.currentState == XmlState.ClosingTagEnded) {
                    xmlParser.currentState = XmlState.ClosingTagStarted;
                    processClosingTagStart();
                    //ignore next character (which is /)
                    return currentIndex + 1;
                } else {
                    throw new InvalidDataException($"Unexpected </ in XML at {currentIndex}");
                }
                
            } else {
                if(xmlParser.currentState == XmlState.OpeningTagEnded ||  xmlParser.currentState == XmlState.ClosingTagEnded) {
                    if(xmlParser.currentText.ToString().Length != 0) {
                        throw new InvalidDataException($"Unexpected string '{xmlParser.currentText}' in XML at {currentIndex - 1}");
                    }
                    xmlParser.currentState = XmlState.OpeningTagStarted;
                } else {
                    throw new InvalidDataException($"Unexpected < in XML at {currentIndex}");
                }
            }

            return currentIndex;
        }

        private void processClosingTagStart() {
            //Tag content should be handled here...
            xmlParser.currentText.Clear();
        }
    }
}