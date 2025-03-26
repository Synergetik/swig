using System;
using director_nullable_argsNamespace;

public class runme
{
  public class DeriveredTestObject : TestObjectDirected
  {
      public override void onIndex(int? index)
      {
          base.onIndex(index);
          Indexes.Add(index);
      }

      public override void onMessage(string? message)
      {
          base.onMessage(message);
          Messages.Add(message);
      }

      public readonly List<string?> Messages = [];
      public readonly List<int?> Indexes = [];
  }
  
  private static void check(string got, string expected)
  {
    if (got != expected)
      throw new ApplicationException("Failed, got: " + got + " expected: " + expected);
  }
  
  static void Main()
  {
    var testDerivedDirected = new DeriveredTestObject();
    
    testDerivedDirected.nextMessage(); // counter = 0
    testDerivedDirected.nextMessage(); // counter = 1
    testDerivedDirected.nextMessage(); // counter = 2
    testDerivedDirected.nextMessage(); // counter = 3

    // Check Indexes
    check(testDerivedDirected.Indexes.Count.ToString(), "4");
    check(testDerivedDirected.Indexes[0]?.ToString() ?? "null", "null");
    check(testDerivedDirected.Indexes[1]?.ToString() ?? "null", "2");
    check(testDerivedDirected.Indexes[2]?.ToString() ?? "null", "null");
    check(testDerivedDirected.Indexes[3]?.ToString() ?? "null", "4");

    // Check Messages
    check(testDerivedDirected.Messages.Count.ToString(), "4");
    check(testDerivedDirected.Messages[0] ?? "null", "null");
    check(testDerivedDirected.Messages[1] ?? "null", "Hello 2");
    check(testDerivedDirected.Messages[2] ?? "null", "null");
    check(testDerivedDirected.Messages[3] ?? "null", "Hello 4");
  }
}
