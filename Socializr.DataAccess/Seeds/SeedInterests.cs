using SocializR.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocializR.DataAccess.Seeds
{
    static class SeedInterests
    {
        public static void Seed(SocializRContext context)
        {
            if(context.Interests.Any())
            {
                return;
            }

            var interests = new List<Interest>
            {
                new Interest {
                    Name = "Sports",
                    ChildInterests =new List<Interest>
                     {
                         new Interest{ Name="FootBall"},
                         new Interest{Name="Swimming"},
                         new Interest{ Name="Jogging"}
                     }
                },

                new Interest{
                    Name = "Arts",
                    ChildInterests =new List<Interest>
                     {
                         new Interest{ Name="Photography"},
                         new Interest{ Name="Cinematography"},
                         new Interest{ Name="Painting"},
                         new Interest{ Name="Dance"},
                         new Interest{ Name="Architecture"}
                     }
                },

                new Interest{
                    Name = "Science",
                    ChildInterests =new List<Interest>
                     {
                        new Interest
                        {
                            Name="Formal Sciences",
                            ChildInterests=new List<Interest>
                            {
                                new Interest{ Name="Mathematics"},
                                new Interest{ Name="Logic"},
                                new Interest{ Name="Statistics"},
                                new Interest{ Name="Tehoretical Computer Science"},
                            }
                        },

                        new Interest
                        {
                            Name="Natural Sciences",
                            ChildInterests=new List<Interest>
                            {
                                new Interest{ Name="Physics"},
                                new Interest{ Name="Chemistry"},
                                new Interest{ Name="Biology"},
                                new Interest{ Name="Geology"},
                                new Interest{ Name="Meteorology"},
                                new Interest{ Name="Astronomy"}
                            }
                        },


                         new Interest
                         {
                             Name ="Applied Sciences",
                             ChildInterests=new List<Interest>
                             {
                                 new Interest{ Name="Engineering"},
                                 new Interest{ Name="Medicine"},
                                 new Interest{ Name="Computer Science"}
                             }
                         }
                     }
                }
            };

            context.AddRange(interests);
            context.SaveChanges();
        }
    }
}
