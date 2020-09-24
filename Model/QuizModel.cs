using JHchoi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class QuizModel
{
    StreamingCSVLoader fileData = new StreamingCSVLoader();
    Dictionary<int, string> problems = new Dictionary<int, string>();
    Dictionary<int, bool> answers = new Dictionary<int, bool>();

    public void Setup(string _fileName)
    {
        fileData.Load(_fileName + ".CSV", CsvLoaded);
    }

    void CsvLoaded()
    {
        var datas = fileData.GetValue("Index");

        foreach (var o in datas)
        {
            var index = fileData.GetEqualsIndex("Index", o);
            var problem = fileData.GetValue("Problem", index);
            var answer = fileData.GetValue("Answer", index);
            problems.Add(index, problem);
            answers.Add(index, Convert.ToBoolean(answer));
        }
    }

    public string GetProblem(int index)
    {
        return problems[index];
    }

    public bool GetAnswer(int index)
    {
        return answers[index];
    }

    public int GetProblemCount()
    {
        return problems.Count;
    }

}
