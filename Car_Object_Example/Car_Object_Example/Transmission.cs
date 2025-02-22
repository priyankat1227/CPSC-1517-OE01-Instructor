﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Object_Example
{
    public class Transmission
    {
        //enum
        public enum TypeName
        {
            Automatic,
            Manual,
            Standard,
            CVT,
            Electric,
            Unicorn
        }
        //private member fields
        private int _gears;

        //public properties
        public TypeName Type;

        public int Gears
        {
            get { return _gears;}
            set
            {
                if (value <= 12 && value >= 2)
                {
                    _gears = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Gears", value, "ERROR: Gears must be between 2 and 12.");
                }
            }
        }

        public Transmission(TypeName type, int gears)
        {
            Type = type;
            Gears = gears;
        }
        
        public Transmission()
        {

        }

        public override string ToString()
        {
            return $"{Gears} speed {Type} transmission";
        }
    }
}
