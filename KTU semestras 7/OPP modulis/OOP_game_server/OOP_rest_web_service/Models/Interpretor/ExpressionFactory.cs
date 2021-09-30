﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_rest_web_service.Models.Interpretor
{
    public class ExpressionFactory
    {
        public static Expression getExpression(string[] tokens)
        {
            Expression exp = new NullExpression();
            if (tokens.Length == 2)
            {
                if (tokens[0].Equals("get"))
                {
                    exp = new GetExpression(new GetTypeExpression(tokens[1]));
                }

            }
            else if (tokens.Length == 1)
            {
                if (tokens[0].Equals("help"))
                {
                    exp = new HelpExpression(tokens[0]);
                }
                else if (tokens[0].Equals("highscore"))
                {
                    exp = new HighscoreExpression();
                }
                else if (tokens[0].Equals("reset"))
                {
                    exp = new ResetExpression();
                }
            }
            else if (tokens.Length == 3)
            {
                if (tokens[0].Equals("setsize"))
                {
                    exp = new SizeExpression(tokens);
                }
            }
            
            return exp;
        }
    }
}
