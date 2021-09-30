﻿using OOP_rest_web_service.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_rest_web_service.State
{
    public class ChargingState : GeneratorState
    {
        public override void Handle(CenterGenerator context)
        {
            Console.WriteLine("context.color = yellow");
            context.color = Color.Yellow;
            Console.WriteLine("context.generatingObject = null");
            context.generatingObjectType = 0;
        }
    }
}
