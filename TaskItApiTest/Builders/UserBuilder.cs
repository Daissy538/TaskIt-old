using System.Collections.Generic;
using System;
using TaskItApi.Entities;

namespace TaskItApiTest.Builders
{
    public class UserBuilder
    {
        private int ID;
        private string Email ;
        private byte[] PasswordHash ;
        private byte[] PasswordSalt ;
        private string Name ;
        private ICollection<Subscription> Subscriptions = new List<Subscription>() ;

        public UserBuilder WithID(int id)
        {
            this.ID = id;
            return this;
        }

        public UserBuilder WithEmail(string email)
        {
            this.Email = email;
            return this;
        }

        public UserBuilder WithPasswordHash(byte[] passwordHash)
        {
            this.PasswordHash = passwordHash;
            return this;
        }

        public UserBuilder WithPasswordSalt(byte[] passwordSalt)
        {
            this.PasswordSalt = passwordSalt;
            return this;
        }

        public UserBuilder WithName(string name)
        {
            this.Name = name;
            return this;
        }

        public User Build()
        {
            return new User
            {
                ID = ID,
                Email = Email,
                Name = Name,
                PasswordHash = PasswordHash,
                PasswordSalt = PasswordSalt,
                Subscriptions = Subscriptions
            };
        }
    }
}
