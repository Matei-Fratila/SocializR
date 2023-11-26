//using SocializR.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace SocializR.DataAccess.Seeds
//{
//    class SeedRolesPermissions
//    {
//        public void Seed(SocializRContext context)
//        {
//            if (context.Roles.Any())
//            {
//                return;
//            }

//            var roles = new List<Role>
//            {
//                new Role
//                {
//                    Name = "Administrator",
//                    Description = "Can do pretty much anything :)",
//                    RolePermissions=new List<RolePermission>
//                    {
//                        new RolePermission
//                        {
//                            Permission=new Permission
//                            {
//                                Name="CUD City",
//                                Description="Create-Update-Delete a City",
//                            }
//                        },

//                        new RolePermission
//                        {
//                            Permission=new Permission
//                            {
//                                Name="CUD County",
//                                Description="Create-Update-Delete a County",
//                            }
//                        },

//                        new RolePermission
//                        {
//                            Permission=new Permission
//                            {
//                                Name="CUD Interest",
//                                Description="Create-Update-Delete an Interest",
//                            }
//                        },

//                        new RolePermission
//                        {
//                            Permission=new Permission
//                            {
//                                Name="Modify Member",
//                                Description="Can modify profiles of the members",
//                            },

//                        },

//                        new RolePermission
//                        {
//                            Permission=new Permission
//                            {
//                                Name="Delete Member",
//                                Description="Can delete members",
//                            },

//                        },
//                    }
//                },

//                new Role
//                {
//                    Name="Member",
//                    Description = "A user of this application with an account",
//                    RolePermissions=new List<RolePermission>
//                    {
//                        new RolePermission
//                        {
//                            Permission=new Permission
//                            {
//                                Name="Modify Personal Data",
//                                Description="Can change their personal data on their profile",
//                            },

//                        },

//                        new RolePermission
//                        {
//                            Permission=new Permission
//                            {
//                                Name="Modify Personal Data",
//                                Description="Can change their personal data on their profile",
//                            },

//                        },

//                        new RolePermission
//                        {
//                            Permission=new Permission
//                            {
//                                Name="Can Post",
//                                Description="Can post things to the feed",
//                            },

//                        },

//                        new RolePermission
//                        {
//                            Permission=new Permission
//                            {
//                                Name="Modify their Visibility",
//                                Description="Can change their profile visibility to PUBLIC/PRIVATE",
//                            },

//                        },

//                        new RolePermission
//                        {
//                            Permission=new Permission
//                            {
//                                Name="Modify Interests",
//                                Description="Can change their list of interests",
//                            },

//                        },

//                        new RolePermission
//                        {
//                            Permission=new Permission
//                            {
//                                Name="Can Comment",
//                                Description="Can add comments to the article in their feed",
//                            },

//                        },

//                        new RolePermission
//                        {
//                            Permission=new Permission
//                            {
//                                Name="Can Like",
//                                Description="Can like posts on their feed",
//                            },

//                        },
//                    }
//                }
//            };

//            context.AddRange(roles);
//            context.SaveChanges();
//        }
//    }
//}
