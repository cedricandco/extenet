using Extenet.Models;

namespace Extenet.Data;

public static class DbInitializer
{
    public static void Initialize(SchoolContext context)
    {
        // Look for any students.
        if (context.Clients.Any())
        {
            return;   // DB has been seeded
        }

        int clientCount = 25000;
        int vendorCount = 25000;
        int itemCount = 20000;
        int salesCount = 20000;
        var clients = new List<Client>();
        var officeAssigments = new List<OfficeAssignment>();

        for (var i = 0; i < clientCount; i++)
        {
            var firstName = string.Empty;
            var lastName = string.Empty;

            if (i % 9 == 0)
            {
                firstName += $"Meredith 9";
                lastName += $"Alonso - {i}";
            }
            else if (i % 8 == 0)
            {
                firstName += $"Jonh 8";
                lastName += $"Doe - {i}";
            }
            else if (i % 7 == 0)
            {
                firstName += $"Gytis 7";
                lastName += $"Barzdukas - {i}";
            }
            else if (i % 7 == 0)
            {
                firstName += $"Gytis 7";
                lastName += $"Barzdukas - {i}";
            }
            else if (i % 6 == 0)
            {
                firstName += $"Yan 6";
                lastName += $"Li - {i}";
            }
            else if (i % 5 == 0)
            {
                firstName += $"Arturo 5";
                lastName += $"Anand - {i}";
            }
            else if (i % 4 == 0)
            {
                firstName += $"Laura 4";
                lastName += $"Normand - {i}";
            }
            else if (i % 3 == 0)
            {
                firstName += $"Lisa 3";
                lastName += $"Secord - {i}";
            }
            else if (i % 2 == 0)
            {
                firstName += $"Robert 2";
                lastName += $"Francois - {i}";
            }
            else if (i % 1 == 0)
            {
                firstName += $"Philippe 1";
                lastName += $"Maurice - {i}";
            }


            clients.Add(new Client
            {
                FirstMidName = firstName,
                LastName = lastName,
                EnrollmentDate = DateTime.Now
            });
        }

        for (var i = 0; i < vendorCount; i++)
        {
            var firstName = string.Empty;
            var lastName = string.Empty;
            var location = "Smith 17";
            if (i % 9 == 0)
            {
                firstName += $"Albert 9";
                lastName += $"Monaco - {i}";
                location = "Rome 9";
            }
            else if (i % 8 == 0)
            {
                firstName += $"Gerard 8";
                lastName += $"Jugnot - {i}";
                location = "Berlin 9";
            }
            else if (i % 7 == 0)
            {
                firstName += $"Jean Claude 7";
                lastName += $"Dus - {i}";
                location = "Calgary 8";
            }
            else if (i % 7 == 0)
            {
                firstName += $"Francis 7";
                lastName += $"Bertrand - {i}";
                location = "Winnipeg 7";
            }
            else if (i % 6 == 0)
            {
                firstName += $"Yann 6";
                lastName += $"Solo - {i}";
                location = "Boston 6";
            }
            else if (i % 5 == 0)
            {
                firstName += $"Roger 5";
                lastName += $"Zend - {i}";
                location = "Vancouver 5";
            }
            else if (i % 4 == 0)
            {
                firstName += $"Candace 4";
                lastName += $"Capor - {i}";
                location = "Gowan 4";
            }
            else if (i % 3 == 0)
            {
                firstName += $"Roger 3";
                lastName += $"Harui - {i}";
                location = "Toronto 3";
            }
            else if (i % 2 == 0)
            {
                firstName += $"Fadi 2";
                lastName += $"Fakhouri - {i}";
                location = "Quebec 2";
            }
            else if (i % 1 == 0)
            {
                firstName += $"Kim 1";
                lastName += $"Abercrombie - {i}";
                location = "Montreal 1";
            }
            officeAssigments.Add(new OfficeAssignment()
            {
                Vendor = new Vendor
                {
                    FirstMidName = firstName,
                    LastName = lastName,
                    HireDate = DateTime.Now
                },
                Location = location
            });
        }

        context.AddRange(officeAssigments);

            var tools = new Department
            {
                Name = "Tools",
                Budget = 350000,
                StartDate = DateTime.Parse("2007-09-01"),
                Administrator = officeAssigments.ElementAt(3).Vendor
            };

            var servers = new Department
            {
                Name = "Servers",
                Budget = 100000,
                StartDate = DateTime.Parse("2007-09-01"),
                Administrator = officeAssigments.ElementAt(5).Vendor
            };

            var engineering = new Department
            {
                Name = "Engineering",
                Budget = 350000,
                StartDate = DateTime.Parse("2007-09-01"),
                Administrator = officeAssigments.ElementAt(30).Vendor
            };

            var development = new Department
            {
                Name = "Development",
                Budget = 100000,
                StartDate = DateTime.Parse("2007-09-01"),
                Administrator = officeAssigments.ElementAt(350).Vendor
            };

            
        

        var items = new List<Item>();
        for (var j = 0; j < itemCount; ++j)
        {
            var department = development;
            if (j % 5 == 0)
            {
                department = engineering;
            }
            else if (j % 4 == 0)
            {
                department = servers;
            }
            else if (j % 3 == 0)
            {
                department = tools;
            }

            items.Add(new Item
            {
                ItemID = j,
                Title = $"Item - {j}",
                Price = j + (50 * (j % 8)),
                Department = department,
                Vendors = new List<Vendor> { officeAssigments.ElementAt(j).Vendor, officeAssigments.ElementAt(j + 1).Vendor }
            });
        }


        var sales = new List<Sale>();
        for (var j = 0; j < salesCount; ++j)
        {
            sales.Add(new Sale
            {
                Client = clients.ElementAt(j),
                Item = items.ElementAt(j)
            });

            var modulo = j % 7;

            if (j + modulo < items.Count())
            {
                sales.Add(new Sale
                {
                    Client = clients.ElementAt(j),
                    Item = items.ElementAt(j + modulo)
                });
            }
        }

        context.AddRange(sales);
        context.SaveChanges();
    }
}
