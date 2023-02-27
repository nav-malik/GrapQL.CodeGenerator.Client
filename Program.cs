using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace GraphQL.Code.Generator.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Configuration.UseDataAnnotationsToFindKeys = true;
            Configuration.MakeAllFieldsOfViewNullable = true;
            Configuration.ViewNameFilter = new Regex(@"^Vw.*$");
            Configuration.ElementsToGenerate = Configuration.Elements.Types | Configuration.Elements.Repositoy | Configuration.Elements.Query;
            Configuration.ORMType = Configuration.ORMTypes.EFCore;

            Configuration.TypeClasses.RepositoryConstructorParameter
                = "RepositoryNamespace.IReposiroty repository";
            Configuration.TypeClasses.RepositoryPrivateMember
                = "RepositoryNamespace.IReposiroty _repository";

            Configuration.TypeClasses.TypeClassesNamespace = "TypeClasses.Namespace";
            Configuration.TypeClasses.EntityClassesNamespacesInclude
                = new Regex(@"^NHLStats.Core.Models$|^HobbyDataLayer.Models$|^PizzaOrder.Data.Entities$", RegexOptions.IgnoreCase);
            Configuration.TypeClasses.EntityClassNamesExclude =
                new Regex(@"^Rusp.*$|^DatabaseInitializer$|^DBManager$|^DataSeeder$|.*MainClass.*|.*SubClass.*", RegexOptions.IgnoreCase);

            Configuration.TypeClasses.AdditionalNamespaces = "namespace1, namespace2";

            string pathCore = "";
            string pathFramework = "";
            string pathCore31 = "";

            Configuration.InputDllNameAndPath = pathCore;
            //Configuration.TypeClasses.EntityClassNamesInclude
            //    = new Regex(@"^Hobby$|^Person$", RegexOptions.IgnoreCase);

            Dictionary<string, string> pluralizationFilter = new Dictionary<string, string>();
            pluralizationFilter.Add("Person", "Persons");
            Configuration.PluralizationFilter = pluralizationFilter;

            Configuration.TypeClasses.AdditionalCodeToBeAddedInConstructor = "InitializePartial();";

            Configuration.RepositoryClass.DBContextPrivateMember
                = "Namespace.Interface _context"; // _context is variablename
            Configuration.RepositoryClass.DBContextConstructorParameter
                = "Namespace.Interface context"; // context is variablename
            Configuration.RepositoryClass.RepositoryClassName = "RepositoryClassName";
            Configuration.RepositoryClass.RepositoryClassesNamespace = "Namespace.Repository";
            Configuration.RepositoryClass.AdditionalNamespaces = "namespace1, namespace2";
            //Configuration.RepositoryClass.MethodExcludeFilter = new List<string> { "MethodName" };
            //Configuration.RepositoryClass.IsMethodExcludeFilterApplyToInterface = true;

            Configuration.QueryClass.RepositoryConstructorParameter
                = "Namespace.Interface variableName";
            Configuration.QueryClass.RepositoryPrivateMember
                = "Namespace.Interface variableName";
            Configuration.QueryClass.QueryClassName = "IBCDMQuery";
            Configuration.QueryClass.QueryClassNamespace = "Graph.Queries";
            Configuration.QueryClass.AdditionalNamespaces = "namespace1, namespace2";
            Configuration.TypeClasses.Outputpath += @"\";
            Configuration.RepositoryClass.Outputpath += @"\";
            Configuration.QueryClass.Outputpath += @"\";
            int fileCount = GraphQL.Code.Generator.GraphQLCodeGenerator.GenerateGraphQLCode();
            Console.WriteLine(fileCount + " GraphQL files generated.");
            Console.ReadLine();
        }
    }
}
