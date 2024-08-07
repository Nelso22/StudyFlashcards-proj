using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flashcards.Models
{

    public class Stack
    {
        public int StackId { get; set; }

        public string? StackName { get; set; }

    }

    public class StackDto
    {
        public string? StackName { get; set; }

    }


    public static class StackMapping
    {
        public static StackDto ToStackDto(this Stack stack)
        {
            return new StackDto
            {
                StackName = stack.StackName
            };
        }
    }

}