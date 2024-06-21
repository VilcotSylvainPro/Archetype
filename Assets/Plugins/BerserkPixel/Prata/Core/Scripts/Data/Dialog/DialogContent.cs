using System;

namespace BerserkPixel.Prata.Data
{
    [Serializable]
    public class DialogContent
    {
        public string characterID;
        public ActorsEmotions emotion;
        public string DialogText = "New Dialog";

        public void Fill(
            string character,
            ActorsEmotions actorEmotion,
            string text
        )
        {
            characterID = character;
            emotion = actorEmotion;
            DialogText = text;
        }
    }
}