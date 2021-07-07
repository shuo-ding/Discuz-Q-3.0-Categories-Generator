/* Copyright (c) 2021 Dr Shuo Ding
  5 July 2021
  Discuz Q 3.0 Categories Generators   
  Author Dr Shuo Ding, 2021  
  Website: https://www.IoTNextDay.com  
  Email: Shuo.Ding.Australia@Gmail.com  
  All Right Reserved 
  Distribution of the copy of this software must include this copyright information   
*/

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Data;
using System.Collections.Generic;

public class LogFile
{
    public StreamWriter sw;
    public void Close()
    {
        sw.Close();
    }
    public void Flush()
    {
        sw.Flush();
    }
    public LogFile(string name)
    {
        sw = new StreamWriter(name);
    }
    public void Write(string str)
    {
        //  Console.WriteLine(str);
        sw.WriteLine(str);
    }
}

class SubTopic
{
    public int id;
    public int priority;
    public int parentid;
    public string name;
    public sqlstring sqlst = new sqlstring();
    public string Show()
    {
        string res = "---------------   " + name + " id " + id + " parent " + parentid + "  priority " + priority;
        Console.WriteLine(res);

        string sqlres = sqlst.returnstring(id, name, name, priority, parentid, 2000, "", "");
        return sqlres;
    }
}

class Topic
{
    public int id;
    public int priority;
    public int parentid;
    public string name;
    public sqlstring sqlst = new sqlstring();

    public List<SubTopic> subtopiclist;

    public Topic()
    {
        subtopiclist = new List<SubTopic>();
        parentid = 0;
    }

    public string Show()
    {
        string res = "   " + name + " id " + id + " parent " + parentid + "  priority " + priority;
        Console.WriteLine(res);

        string sqlres = sqlst.returnstring(id, name, name, priority, parentid, 2000, "", "");
        return sqlres;

    }
}

class sqlstring
{
    public string myid = "";
    public string myname = "";
    public string mydescription = "";
    public string mysort = "";
    public string myparentid = "";
    public string mythreadcount = "";
    public string mymoduler = "";
    public string myip = "101.131.141.231";
    public string mysql = "insert into categories (id, name , description , icon, sort,property,thread_count,moderators, ip, parentid, created_at, updated_at) values ('myid','myname','mydescription','','mysort','0','mythreadcount','mymoduler','myip','myparentid',current_time(),current_time());";
    public string returnstring(int id, string name, string desc, int sort, int parent, int count, string moduler, string ip)
    {
        myid = id.ToString();
        myname = name;
        mydescription = desc;
        mysort = sort.ToString();
        myparentid = parent.ToString();
        mymoduler = "mymoderators";
        myip = "101.131.141.231";
        Random myrand = new Random();
        mythreadcount = myrand.Next(count / 3, count).ToString();
        mydescription = name;
        return
            mysql.Replace("myid", myid).Replace("myname", myname).Replace("mythreadcount", mythreadcount).Replace("mydescription", mydescription).Replace("mysort", mysort).Replace("myparentid", myparentid).Replace("mymoduler", mymoduler).Replace("myip", myip);
    }
}

class Program
{
    static void Main(string[] args)
    {
        LogFile log = new LogFile("mysqlscript.txt");
        int counter = 0;
        string line;
        List<string> mylist = new List<string>();

        // Read the file and display it line by line.  
        System.IO.StreamReader file =
            new System.IO.StreamReader("config.txt");
        while ((line = file.ReadLine()) != null)
        {
            string newline = line.Trim();

            if (newline.Length != 0)
            {
                mylist.Add(newline);

            }
            counter++;
        }

        file.Close();
        System.Console.WriteLine("There were {0} lines.", counter);
        // Suspend the screen.
        // 
        List<Topic> topiclist = new List<Topic>();
        int id = 1;
        int priority = 0;

        for (int i = 0; i < mylist.Count; i++)
        {
            if (mylist[i] == "end")
            {
                if (i == mylist.Count - 1)
                    break;
                i++;
                Topic newtopic = new Topic();
                newtopic.name = mylist[i];
                newtopic.id = id;
                newtopic.priority = priority;
                priority++;
                id++;
                int subpriority = 0;
                topiclist.Add(newtopic);
                int j = i + 1;
                while (mylist[j] != "end" && j < mylist.Count - 1)
                {
                    SubTopic subtopic = new SubTopic();
                    subtopic.name = mylist[j];
                    subtopic.id = id;
                    subtopic.parentid = newtopic.id;
                    subtopic.priority = subpriority;
                    subpriority++;
                    id++;
                    newtopic.subtopiclist.Add(subtopic);
                    j++;
                }
            }
        }

        foreach (Topic ele in topiclist)
        {
            log.Write(ele.Show());


            foreach (SubTopic subele in ele.subtopiclist)
            {
                log.Write(subele.Show());
            }
        }
        log.Flush();
        log.Close();
    }
}


