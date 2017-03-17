// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Modifications Copyright (c) 2017 Edward Blair. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace IdentityServer4.MongoDBDriver.Entities
{
    public class ApiResource
    {
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public ICollection<Secret> ApiSecrets { get; set; }
        public ICollection<string> UserClaims { get; set; }
        public ICollection<Scope> Scopes { get; set; }
    }
}
