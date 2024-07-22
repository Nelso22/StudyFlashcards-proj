using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flashcards.Models
{
    public class FlashcardModel
    {
        public int FlashcardId { get; set; }

        public int StackId { get; set; }

        public string? Question { get; set; }

        public string? Answer { get; set; }

    }

    public class FlashcardDto
    {
        public string? Question { get; set; }

        public string? Answer { get; set; }

    }


    public static class FlashcardMapping
    {
        public static FlashcardDto ToFlashcardDto(this FlashcardModel flashcard)
        {
            return new FlashcardDto
            {
                Question = flashcard.Question,
                Answer = flashcard.Answer

            };
        }
    }


}


