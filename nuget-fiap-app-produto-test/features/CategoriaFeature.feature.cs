﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace nuget_fiap_app_produto_test.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class GerenciamentoDeCategoriasFeature : object, Xunit.IClassFixture<GerenciamentoDeCategoriasFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "CategoriaFeature.feature"
#line hidden
        
        public GerenciamentoDeCategoriasFeature(GerenciamentoDeCategoriasFeature.FixtureData fixtureData, nuget_fiap_app_produto_test_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("pt"), "features", "Gerenciamento de Categorias", @"    Como um usu�rio da API
    Eu quero gerenciar categorias

Cen�rio: Obter todas as categorias
    Dado que eu adicionei uma categoria com o nome ""Comida""
    Quando eu solicito a lista de categorias
    Ent�o eu devo receber uma lista contendo ""Comida""

Cen�rio: Adicionar uma nova categoria
    Quando eu adiciono uma categoria com o nome ""Doces""
    Ent�o a categoria ""Doces"" deve ser adicionada com sucesso

Cen�rio: Obter categoria por ID
    Dado que eu adicionei uma categoria com o nome ""Molhos""
    Quando eu solicito a categoria pelo seu ID
    Ent�o eu devo receber a categoria ""Molhos""

Cen�rio: Atualizar uma categoria existente
    Dado que eu adicionei uma categoria com o nome ""Oriental""
    E eu atualizo a categoria ""Oriental"" para ter o nome ""Comida Japonesa""
    Quando eu solicito a categoria pelo seu ID
    Ent�o eu devo receber a categoria com o nome ""Comida Japonesa""

Cen�rio: Excluir uma categoria
    Dado que eu adicionei uma categoria com o nome ""Comida""
    Quando eu excluo a categoria ""Comida""
    Ent�o a categoria ""Comida"" n�o deve mais existir", ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                GerenciamentoDeCategoriasFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                GerenciamentoDeCategoriasFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
