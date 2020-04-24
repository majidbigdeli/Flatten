using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var ff = new IVRTree()
            {
                NextCode = null,
                Value = " سلام اقای بیگدلی جهت اطلاع از وضعیت باغ ها کلید 1 جهت اطلاع از میزان بارش  کلید2 و جهت اتصال به اپراتور کلید 3 را بفشارید .",
                IVRBranches = new List<IVRBranch>()
                {
                    new IVRBranch()
                    {
                        ISBack = false,
                        Key = "1",
                        IVRTree = new IVRTree()
                        {
                            Value = "جهت اطلاع از باغ همدان کلید یک و شهریار کلید 2 وجهن بازگشت کلید3 را بزنید",
                            NextCode = null,
                            IVRBranches = new List<IVRBranch>()
                            {
                               new IVRBranch()
                               {
                                   ISBack = false,
                                   Key = "1",
                                   IVRTree = new IVRTree()
                                   {
                                       Value = "خوب"  ,
                                       NextCode = "H"

                                   }
                               },
                                new IVRBranch()
                               {
                                   ISBack = false,
                                   Key = "2",
                                   IVRTree = new IVRTree()
                                   {
                                       NextCode = "E/101",
                                       Value = "بد"
                                   }
                               },
                               new IVRBranch()
                               {
                                   ISBack = true,
                                   Key = "3",
                               }

                            }
                        }
                    },
                     new IVRBranch()
                    {
                        ISBack = false,
                        Key = "2",
                        IVRTree = new IVRTree()
                        {
                            Value = "AB",
                            NextCode = null,
                            IVRBranches = new List<IVRBranch>()
                            {
                                new IVRBranch()
                                {
                                    ISBack = false,
                                    Key = "1",
                                    IVRTree = new IVRTree()
                                    {
                                        Value = "",
                                        NextCode =""
                                    }
                                }
                            }
                        }
                    },
                    new IVRBranch()
                    {
                        ISBack = false,
                        Key = "3",
                        IVRTree = new IVRTree()
                        {
                            Value = "AC",
                            NextCode = "E/102"
                        }
                    }
                }

            };

            var ii = flatten(ff, new List<FlatModel>(), null);

        }

        private static List<FlatModel> flatten(IVRTree node, List<FlatModel> flatList, Guid? parentId)
        {

            if (node != null)
            {
                if (flatList.Count == 0)
                {
                    var _id = Guid.NewGuid();

                    FlatModel n = new FlatModel()
                    {
                        Id = _id,
                        Value = node.Value,
                        NextCode = node.NextCode,
                        ISBack = false,
                        ParentId = parentId,
                        Key = null,
                        HasBranch = node.IVRBranches?.Count > 0
                    };
                    flatList.Add(n);

                    if (node.IVRBranches != null)
                    {
                        foreach (var item in node.IVRBranches)
                        {
                            var _Id2 = Guid.NewGuid();
                            FlatModel n1 = new FlatModel()
                            {
                                Id = _Id2,
                                Value = item.IVRTree?.Value,
                                NextCode = item.IVRTree?.NextCode,
                                ISBack = item.ISBack,
                                ParentId = _id,
                                Key = item.Key,
                                HasBranch = item.IVRTree?.IVRBranches?.Count > 0

                            };
                            flatList.Add(n1);
                            flatten(item.IVRTree, flatList, _Id2);
                        }
                    }


                }
                else
                {
                    if (node.IVRBranches != null)
                    {
                        foreach (var item in node.IVRBranches)
                        {
                            var _Id2 = Guid.NewGuid();
                            FlatModel n1 = new FlatModel()
                            {
                                Id = _Id2,
                                Value = item.IVRTree?.Value,
                                NextCode = item.IVRTree?.NextCode,
                                ISBack = item.ISBack,
                                ParentId = parentId,
                                Key = item.Key,
                                HasBranch = item.IVRTree?.IVRBranches?.Count > 0
                            };
                            flatList.Add(n1);
                            flatten(item.IVRTree, flatList, _Id2);
                        }

                    }
                }
            }
            return flatList;


            //if (node != null)
            //{
            //    var _id = Guid.NewGuid();
            //    MyNode n = new MyNode(_id, node.Id, node.Name);
            //    flatList.Add(n);

            //    if (node.Nodes != null)
            //    {
            //        foreach (var item in node.Nodes)
            //        {
            //            item.SetID(_id);
            //        }


            //        foreach (var child in node.Nodes)
            //        {
            //            flatten(child, flatList);
            //        }
            //    }

            //}

        }


    }


    public class IVRTree
    {

        public string Value { get; set; }
        public string NextCode { get; set; }
        public List<IVRBranch> IVRBranches { get; set; }

    }

    public class IVRBranch
    {
        public string Key { get; set; }
        public bool ISBack { get; set; }
        public IVRTree IVRTree { get; set; }
    }



    public class FlatModel
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string Value { get; set; }
        public string NextCode { get; set; }
        public string Key { get; set; }
        public bool ISBack { get; set; }
        public bool HasBranch { get; set; }
    }



}
