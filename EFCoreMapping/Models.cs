using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMapping
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public Address Address { get; set; }

        [InverseProperty("Owner")]
        public List<Account> Accounts { get; set; }

        public List<Phone> Phones { get; set; }
    }

    [Owned]
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        [Column(TypeName = "char(5)")]
        public string Zip { get; set; }
    }

    [Table("Phones")]
    public class Phone
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [MaxLength(32)]
        public string Number { get; set; }
    }

    public class Account
    {
        public int AccountId { get; set; }

        public decimal Balance { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public int? OwnerId { get; set; }
        public Customer Owner { get; set; }

        public List<AccountCoOwner> CoOwners { get; set; }

        public bool Deleted { get; set; } = false;
    }

    [Table("AccountCoOwners")]
    public class AccountCoOwner
    {
        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int CoOwnerId { get; set; }
        public Customer CoOwner { get; set; }
    }

    public class CDAccount : Account
    {
        public int Term { get; set; }
    }
}
