// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------------------- 

using Microsoft.Azure.Mobile.Server;

namespace Local.Models
{
    public class Person : StorageData
    {
        public Person()
        {
        }

        public Person(string partitionKey, string rowKey) : base(partitionKey, rowKey)
        {
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }
    }
}