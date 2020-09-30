using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public static class ForGeneration
    {
        public static MyModel Model = new MyModel
        {
            Namespace = "AAA",
            Types = new[]
            {
                new MyType
                {
                    Name = "T1",
                    Properties = new[]
                    {
                        new MyProperty
                        {
                            Name = "P1",
                            Type = "string"
                        },
                        new MyProperty
                        {
                            Name = "P2",
                            Type = "int"
                        },
                    }
                }
            }
        };
    }
}
