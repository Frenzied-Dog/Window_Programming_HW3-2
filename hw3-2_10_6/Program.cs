using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace hw3_2_10_6 {
    internal class Program {
        class Member {
            public Member(string n, string d, string i) {
                name = n;
                department = d;
                ID = i;
                level = 1;
                Title = "無";
            }
            public int level;
            public string name, department, ID, Title;
            static public bool operator ==(Member a, Member b) {
                return a.name.Equals(b.name) && a.department.Equals(b.department) && a.ID.Equals(b.ID);
            }
            static public bool operator !=(Member a, Member b) {
                return !(a == b);
            }

            public override bool Equals(object? obj) {
                if (ReferenceEquals(this, obj)) return true;
                if (obj is null) return false;
                throw new NotImplementedException();
            }

            public override int GetHashCode() {
                throw new NotImplementedException();
            }
        }

        static void PrintHelp() {
            Console.WriteLine("新增社員資訊:\tregister  name\tdepartment  ID");
            Console.WriteLine("以特定屬性查詢:\tsearch\t  tag\tWant_search_string");
            Console.WriteLine("授予社員職位:\tentitle\t  name\tdepartment  ID\tThat_Title");
            Console.WriteLine("所有社員列表:\tcheck");
            Console.WriteLine("指令格式列表:\thelp");
            Console.WriteLine("離開此程式:\texit");
        }

        static string LevelToStr(int lev) {
            return (lev == 1 ? "萌新社員" : lev == 2 ? "資深社員" : "永久社員");
        }

        static void Main(string[] args) {
            bool exit = false;
            List<Member> memList = new();
            Console.WriteLine("--------------------###   社員資料登記   ###--------------------");
            PrintHelp();
            while (!exit) {
                Console.Write("> ");
                string[] cmds;
                cmds = Console.ReadLine().Split(' ');

                switch (cmds[0]) {
                case "register": {
                    Member tmp = new(cmds[1], cmds[2], cmds[3]);
                    Member? found = memList.Find(x => x == tmp);
                    if (found is not null) {
                        if (found.level < 3)
                            Console.WriteLine($"已晉升為{(++found.level == 2 ? "資深社員" : "永久社員")}");
                        else Console.WriteLine("已經是永久社員了喔");
                    } else {
                        memList.Add(tmp);
                        Console.WriteLine("歡迎新社員!!");
                    }
                    break;
                }
                case "search": {
                    List<Member> result;
                    switch (cmds[1]) {
                    case "name":
                        result = memList.FindAll(x => x.name == cmds[2]);
                        if (result.Count == 0)
                            Console.WriteLine("找不到這個人ㄟ");
                        else foreach (Member m in result)
                                Console.WriteLine($"{m.name}\t{m.department}\t{m.ID}\t{LevelToStr(m.level)}\t{m.Title}");
                        break;
                    case "department":
                        result = memList.FindAll(x => x.department == cmds[2]);
                        if (result.Count == 0)
                            Console.WriteLine("找不到這個系的人ㄟ");
                        else foreach (Member m in result)
                                Console.WriteLine($"{m.name}\t{m.department}\t{m.ID}\t{LevelToStr(m.level)}\t{m.Title}");
                        break;
                    case "ID":
                        result = memList.FindAll(x => x.ID == cmds[2]);
                        if (result.Count == 0)
                            Console.WriteLine("找不到這個學號的人ㄟ");
                        else foreach (Member m in result)
                                Console.WriteLine($"{m.name}\t{m.department}\t{m.ID}\t{LevelToStr(m.level)}\t{m.Title}");
                        break;
                    case "level":
                        result = memList.FindAll(x => LevelToStr(x.level) == cmds[2]);
                        if (result.Count == 0)
                            Console.WriteLine("找不到這個等級的人ㄟ");
                        else foreach (Member m in result)
                                Console.WriteLine($"{m.name}\t{m.department}\t{m.ID}\t{LevelToStr(m.level)}\t{m.Title}");
                        break;
                    case "title":
                        result = memList.FindAll(x => x.Title == cmds[2]);
                        if (result.Count == 0)
                            Console.WriteLine("找不到這個職務的人ㄟ");
                        else foreach (Member m in result)
                                Console.WriteLine($"{m.name}\t{m.department}\t{m.ID}\t{LevelToStr(m.level)}\t{m.Title}");
                        break;
                    default:
                        Console.WriteLine("\t不存在這個標籤，若有疑慮請打help");
                        break;
                    }
                    break;
                }
                case "entitle": {
                    Member? found = memList.Find(x => x == new Member(cmds[1], cmds[2], cmds[3]));
                    if (found is not null) {
                        if (cmds[4].Contains("社長")) Console.WriteLine("\t我們的社長只有阿明一個人，你不要想篡位!");
                        else {
                            found.Title = cmds[4];
                            Console.WriteLine($"\t{found.name}已晉升為 {cmds[4]} !");
                        }
                    } else Console.WriteLine("找不到這個人ㄟ");
                    break;
                }
                case "check":
                    Console.WriteLine("----------------------------------------------------------------");
                    foreach (Member m in memList) {
                        Console.WriteLine($"{m.name}\t{m.department}\t{m.ID}\t{LevelToStr(m.level)}\t{m.Title}");
                    }
                    Console.WriteLine("----------------------------------------------------------------");
                    break;
                case "help":
                    Console.WriteLine("----------------------------------------------------------------");
                    PrintHelp();
                    Console.WriteLine("----------------------------------------------------------------");
                    break;
                case "exit":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("\t不存在這個功能，若有疑慮請打help");
                    break;
                }
                Console.WriteLine();
            }
        }
    }
}