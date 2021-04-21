using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AssetBundleBrowser.AssetBundleModel;
using Assets.AssetBundles_Browser.Editor.Tests.Util;
using Assets.Editor.Tests.Util;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace AssetBundleBrowserTests
{
  internal class ABModelTests
  {
    private List<BundleInfo> m_BundleInfo;

    [SetUp]
    public void Setup()
    {
      AssetDatabase.RemoveUnusedAssetBundleNames();
      Model.Rebuild();

      m_BundleInfo = new List<BundleInfo>();
      m_BundleInfo.Add(new BundleDataInfo("1bundle1", null));
      m_BundleInfo.Add(new BundleDataInfo("2bundle2", null));
      m_BundleInfo.Add(new BundleDataInfo("3bundle3", null));
    }

    [TearDown]
    public static void TearDown()
    {
      var gameObjectsInScene = Object.FindObjectsOfType<GameObject>()
        .Where(go => go.tag != "MainCamera").ToArray();

      foreach (var obj in gameObjectsInScene) Object.DestroyImmediate(obj, false);
    }

    [Test]
    public void AddBundlesToUpdate_AddsCorrectBundles_ToUpdateQueue()
    {
      // Account for existing asset bundles
      var numBundles = ABModelUtil.BundlesToUpdate.Count;

      Model.AddBundlesToUpdate(m_BundleInfo);
      Assert.AreEqual(numBundles + 3, ABModelUtil.BundlesToUpdate.Count);
    }

    [Test]
    public void ModelUpdate_LastElementReturnsTrueForRepaint()
    {
      // Clear out existing data
      var numChildren = ABModelUtil.Root.GetChildList().Count;
      for (var i = 0; i <= numChildren; ++i)
        if (Model.Update())
          break;

      // Step through updates for the test bundle info, last element should require repaint
      Model.AddBundlesToUpdate(m_BundleInfo);
      Assert.IsFalse(Model.Update());
      Assert.IsFalse(Model.Update());
      Assert.IsTrue(Model.Update());
    }

    [Test]
    public void ModelRebuild_Clears_BundlesToUpdate()
    {
      // Account for existing bundles
      var numChildren = ABModelUtil.Root.GetChildList().Count;

      Model.AddBundlesToUpdate(m_BundleInfo);
      Model.Rebuild();
      Assert.AreEqual(numChildren, ABModelUtil.BundlesToUpdate.Count);
    }

    [Test]
    public void ModelUpdate_ReturnsFalseForRepaint()
    {
      Model.AddBundlesToUpdate(m_BundleInfo);
      Assert.IsFalse(Model.Update());
    }

    [Test]
    public static void ValidateAssetBundleListMatchesAssetDatabase()
    {
      var numBundles = AssetDatabase.GetAllAssetBundleNames().Length;

      var list = Model.ValidateBundleList();
      Assert.AreEqual(numBundles, list.Length);
    }

    [Test]
    public static void ValidateAssetBundleList_ReturnsCorrect_ListOfBundles()
    {
      // Account for existing bundles
      var numBundles = AssetDatabase.GetAllAssetBundleNames().Length;

      var listOfPrefabs = new List<string>();
      var bundleName = "bundletest";

      //Arrange: Create a prefab and set it's asset bundle name
      listOfPrefabs.Add(TestUtil.CreatePrefabWithBundleAndVariantName(bundleName, string.Empty));

      TestUtil.ExecuteCodeAndCleanupAssets(() =>
      {
        //Act: Operates on the list of asset bundle names found in the AssetDatabase
        var list = Model.ValidateBundleList();

        //Assert
        Assert.AreEqual(numBundles + 1, list.Length);
        Assert.IsTrue(list.Contains(bundleName));
      }, listOfPrefabs);
    }

    [Test]
    public static void ValidateAssetBundleList_WithVariants_ContainsCorrectList()
    {
      // Account for existing bundles
      var numBundles = AssetDatabase.GetAllAssetBundleNames().Length;

      var listOfPrefabs = new List<string>();

      var bundleName = "bundletest";

      listOfPrefabs.Add(TestUtil.CreatePrefabWithBundleAndVariantName(bundleName, "v1"));
      listOfPrefabs.Add(TestUtil.CreatePrefabWithBundleAndVariantName(bundleName, "v2"));

      TestUtil.ExecuteCodeAndCleanupAssets(() =>
      {
        //Act: Operates on the list of asset bundle names found in the AssetDatabase
        var list = Model.ValidateBundleList();

        //Assert
        Assert.AreEqual(numBundles + 2, list.Length);
        Assert.IsTrue(list.Contains(bundleName + ".v1"));
        Assert.IsTrue(list.Contains(bundleName + ".v2"));
      }, listOfPrefabs);
    }

    [Test]
    public static void ModelRebuild_KeepsCorrect_BundlesToUpdate()
    {
      // Account for existing bundles
      var numChildren = ABModelUtil.Root.GetChildList().Count;

      var listOfPrefabs = new List<string>();

      var bundleName = "bundletest";

      listOfPrefabs.Add(TestUtil.CreatePrefabWithBundleAndVariantName(bundleName, "v1"));
      listOfPrefabs.Add(TestUtil.CreatePrefabWithBundleAndVariantName(bundleName, "v2"));

      TestUtil.ExecuteCodeAndCleanupAssets(() =>
      {
        Model.Rebuild();

        var rootChildList = ABModelUtil.Root.GetChildList();

        //Checks that the root has 1 bundle variant folder object as a child
        Assert.AreEqual(numChildren + 1, rootChildList.Count);

        var variantFolderType = typeof(BundleVariantFolderInfo);
        BundleVariantFolderInfo foundItem = null;
        foreach (var item in rootChildList)
          if (item.GetType() == variantFolderType)
          {
            foundItem = item as BundleVariantFolderInfo;
            break;
          }

        //Checks that the bundle variant folder object (mentioned above) has two children
        Assert.IsNotNull(foundItem);
        Assert.AreEqual(2, foundItem.GetChildList().Count);
      }, listOfPrefabs);
    }

    [Test]
    public static void VerifyBasicTreeStructure_ContainsCorrect_ClassTypes()
    {
      // Account for existing bundles
      var numChildren = ABModelUtil.Root.GetChildList().Count;

      var listOfPrefabs = new List<string>();
      var bundleName = "bundletest";

      listOfPrefabs.Add(TestUtil.CreatePrefabWithBundleAndVariantName(bundleName, "v1"));
      listOfPrefabs.Add(TestUtil.CreatePrefabWithBundleAndVariantName(bundleName, "v2"));

      TestUtil.ExecuteCodeAndCleanupAssets(() =>
      {
        Model.Refresh();

        var rootChildList = ABModelUtil.Root.GetChildList();
        Assert.AreEqual(numChildren + 1, rootChildList.Count);

        var bundleVariantFolderInfoType = typeof(BundleVariantFolderInfo);
        BundleVariantFolderInfo foundItem = null;
        foreach (var item in rootChildList)
          if (item.GetType() == bundleVariantFolderInfoType)
          {
            foundItem = item as BundleVariantFolderInfo;
            break;
          }

        Assert.IsNotNull(foundItem);

        var folderChildArray = foundItem.GetChildList().ToArray();
        Assert.AreEqual(2, folderChildArray.Length);

        Assert.AreEqual(typeof(BundleVariantDataInfo), folderChildArray[0].GetType());
        Assert.AreEqual(typeof(BundleVariantDataInfo), folderChildArray[1].GetType());
      }, listOfPrefabs);
    }

    [Test]
    public static void CreateEmptyBundle_AddsBundle_ToRootBundles()
    {
      // Account for existing bundles
      var numChildren = GetBundleRootFolderChildCount();

      var bundleName = "testname";
      Model.CreateEmptyBundle(null, bundleName);

      Assert.AreEqual(numChildren + 1, GetBundleRootFolderChildCount());
    }

    [Test]
    public static void CreatedEmptyBundle_Remains_AfterRefresh()
    {
      // Account for existing bundles
      var numChildren = GetBundleRootFolderChildCount();

      //Arrange
      var bundleName = "testname";
      Model.CreateEmptyBundle(null, bundleName);

      //Act
      Model.Refresh();

      //Assert
      Assert.AreEqual(numChildren + 1, GetBundleRootFolderChildCount());
    }

    [Test]
    public static void CreatedEmptyBundle_IsRemoved_AfterRebuild()
    {
      // Account for existing bundles
      var childCount = GetBundleRootFolderChildCount();

      var bundleName = "testname";
      Model.CreateEmptyBundle(null, bundleName);

      Model.Rebuild();

      Assert.AreEqual(childCount, GetBundleRootFolderChildCount());
    }

    [Test]
    public static void MoveAssetToBundle_PlacesAsset_IntoMoveQueue()
    {
      var assetName = "New Asset";
      var listOfPrefabs = new List<string>();

      var bundle1Name = "bundle1";
      var bundle2Name = "bundle2";

      listOfPrefabs.Add(TestUtil.CreatePrefabWithBundleAndVariantName(bundle1Name, string.Empty, assetName));
      listOfPrefabs.Add(TestUtil.CreatePrefabWithBundleAndVariantName(bundle2Name, string.Empty, assetName));

      TestUtil.ExecuteCodeAndCleanupAssets(() =>
      {
        Assert.AreEqual(0, ABModelUtil.MoveData.Count);
        Model.MoveAssetToBundle(assetName, bundle2Name, string.Empty);
        Assert.AreEqual(1, ABModelUtil.MoveData.Count);
      }, listOfPrefabs);
    }

    [Test]
    public static void ExecuteAssetMove_MovesAssets_IntoCorrectBundles_UsingStrings()
    {
      var listOfPrefabs = new List<string>();

      var bundle1Name = "bundle1";
      var bundle2Name = "bundle2";

      listOfPrefabs.Add(TestUtil.CreatePrefabWithBundleAndVariantName(bundle1Name, string.Empty, "Asset to Move"));
      listOfPrefabs.Add(TestUtil.CreatePrefabWithBundleAndVariantName(bundle2Name, string.Empty));

      TestUtil.ExecuteCodeAndCleanupAssets(() =>
      {
        Model.MoveAssetToBundle(listOfPrefabs[0], bundle2Name, string.Empty);
        Model.ExecuteAssetMove();
        Assert.AreEqual(bundle2Name, AssetImporter.GetAtPath(listOfPrefabs[0]).assetBundleName);
        Assert.AreEqual(string.Empty, AssetImporter.GetAtPath(listOfPrefabs[0]).assetBundleVariant);
      }, listOfPrefabs);
    }

    [Test]
    public static void ExecuteAssetMove_MovesAssets_IntoCorrectBundles_UsingAssetInfo()
    {
      var listOfPrefabs = new List<string>();

      var bundle1Name = "bundle1";
      var bundle2Name = "bundle2";

      listOfPrefabs.Add(TestUtil.CreatePrefabWithBundleAndVariantName(bundle1Name, string.Empty, "Asset to Move"));
      listOfPrefabs.Add(TestUtil.CreatePrefabWithBundleAndVariantName(bundle2Name, string.Empty));

      TestUtil.ExecuteCodeAndCleanupAssets(() =>
      {
        var info = Model.CreateAsset(listOfPrefabs[0], bundle1Name);
        Model.MoveAssetToBundle(info, bundle2Name, string.Empty);
        Model.ExecuteAssetMove();
        Assert.AreEqual(bundle2Name, AssetImporter.GetAtPath(listOfPrefabs[0]).assetBundleName);
        Assert.AreEqual(string.Empty, AssetImporter.GetAtPath(listOfPrefabs[0]).assetBundleVariant);
      }, listOfPrefabs);
    }

    [Test]
    public static void CreateAsset_CreatesAsset_WithCorrectData()
    {
      var assetName = "Assets/assetName";
      var bunleName = "bundle1";

      var info = Model.CreateAsset(assetName, bunleName);
      Assert.AreEqual(assetName, info.fullAssetName);
      Assert.AreEqual(bunleName, info.bundleName);
    }

    [Test]
    public static void HandleBundleRename_RenamesTo_CorrectAssetBundleName()
    {
      var bundleDataInfoName = "bundledatainfo";
      var newBundleDataInfoName = "newbundledatainfo";

      var dataInfo = new BundleDataInfo(bundleDataInfoName, ABModelUtil.Root);
      var treeItem = new BundleTreeItem(dataInfo, 0, ABModelUtil.FakeTexture2D);

      var handleBundle = Model.HandleBundleRename(treeItem, newBundleDataInfoName);

      Assert.IsTrue(handleBundle);
      Assert.AreEqual(treeItem.bundle.m_Name.bundleName, newBundleDataInfoName);
    }

    [Test]
    public static void AssetBundleName_GetsRenamed_WhenBundleIsRenamed()
    {
      var listOfPrefabs = new List<string>();

      var bundle1Name = "bundle1";
      var bundle2Name = "bundle2";

      listOfPrefabs.Add(TestUtil.CreatePrefabWithBundleAndVariantName(bundle1Name, string.Empty));

      TestUtil.ExecuteCodeAndCleanupAssets(() =>
      {
        BundleInfo b = new BundleDataInfo(bundle1Name, ABModelUtil.Root);
        var treeItem = new BundleTreeItem(b, 0, ABModelUtil.FakeTexture2D);

        Model.HandleBundleRename(treeItem, bundle2Name);

        Assert.AreEqual(bundle2Name, AssetImporter.GetAtPath(listOfPrefabs[0]).assetBundleName);
      }, listOfPrefabs);
    }

    [Test]
    public static void BundleFolderInfo_ChildrenTable_UpdatesWhenBundleIsRenamed()
    {
      // Account for existing asset bundles
      var numExistingChildren = ABModelUtil.Root.GetChildList().Count;

      var listOfPrefabs = new List<string>();

      var bundle1Name = "bundle1";
      var bundle2Name = "bundle2";

      listOfPrefabs.Add(TestUtil.CreatePrefabWithBundleAndVariantName(bundle1Name, string.Empty));

      TestUtil.ExecuteCodeAndCleanupAssets(() =>
      {
        BundleInfo b = new BundleDataInfo(bundle1Name, ABModelUtil.Root);
        ABModelUtil.Root.AddChild(b);
        var treeItem = new BundleTreeItem(b, 0, ABModelUtil.FakeTexture2D);
        Model.ExecuteAssetMove();

        Assert.AreEqual(bundle1Name, ABModelUtil.Root.GetChildList().ElementAt(numExistingChildren).m_Name.bundleName);
        Model.HandleBundleRename(treeItem, bundle2Name);
        Assert.AreEqual(bundle2Name, ABModelUtil.Root.GetChildList().ElementAt(numExistingChildren).m_Name.bundleName);
      }, listOfPrefabs);
    }

    [Test]
    public static void BundleTreeItem_ChangesBundleName_AfterRename()
    {
      var bundle1Name = "bundle1";
      var bundle2Name = "bundle2";

      BundleInfo b = new BundleDataInfo(bundle1Name, ABModelUtil.Root);
      var treeItem = new BundleTreeItem(b, 0, ABModelUtil.FakeTexture2D);
      Model.HandleBundleRename(treeItem, bundle2Name);
      Assert.AreEqual(bundle2Name, treeItem.bundle.m_Name.bundleName);
    }

    [Test]
    public static void HandleBundleReparent_MovesBundleDataInfoBundles_ToTheCorrectParent()
    {
      var dataInfo = new BundleDataInfo("bundle1", ABModelUtil.Root);
      var concreteFolder = new BundleFolderConcreteInfo("folder1", ABModelUtil.Root);

      ABModelUtil.Root.AddChild(dataInfo);
      ABModelUtil.Root.AddChild(concreteFolder);

      Model.HandleBundleReparent(new BundleInfo[] { dataInfo }, concreteFolder);

      Assert.AreEqual(dataInfo.parent.m_Name.bundleName, concreteFolder.m_Name.bundleName);
    }

    [Test]
    public static void HandleBundleReparent_MoveBundleFolderInfo_ToTheCorrectParent()
    {
      var concreteFolder = new BundleFolderConcreteInfo("folder1", ABModelUtil.Root);
      var subConcreteFolder = new BundleFolderConcreteInfo("subFolder1", concreteFolder);
      var folderToBeMoved = new BundleFolderConcreteInfo("folder2", subConcreteFolder);

      ABModelUtil.Root.AddChild(concreteFolder);
      concreteFolder.AddChild(subConcreteFolder);
      subConcreteFolder.AddChild(subConcreteFolder);

      Model.HandleBundleReparent(new BundleInfo[] { folderToBeMoved }, concreteFolder);

      Assert.AreEqual(concreteFolder.m_Name.bundleName, folderToBeMoved.parent.m_Name.bundleName);
    }

    [Test]
    public static void HandleBundleReparent_MovesBundleVariant_ToCorrectParent()
    {
      var concreteFolder = Model.CreateEmptyBundleFolder() as BundleFolderConcreteInfo;
      var subConcreteFolder = Model.CreateEmptyBundleFolder(concreteFolder) as BundleFolderConcreteInfo;
      var startParent = Model.CreateEmptyBundleFolder(subConcreteFolder) as BundleFolderConcreteInfo;

      var bundleVariantDataInfo = new BundleVariantDataInfo("v1", startParent);

      Model.HandleBundleReparent(new BundleInfo[] { bundleVariantDataInfo }, concreteFolder);
      Assert.AreEqual(concreteFolder, bundleVariantDataInfo.parent);
    }

    [Test]
    public static void HandleBundleReparent_MovesBundleFolderVariant_ToCorrectParent()
    {
      var concreteFolder = Model.CreateEmptyBundleFolder() as BundleFolderConcreteInfo;
      var startParent = Model.CreateEmptyBundleFolder() as BundleFolderConcreteInfo;
      var bundleVariantFolder =
        Model.CreateEmptyVariant(new BundleVariantFolderInfo("v1", startParent)) as BundleVariantDataInfo;

      Model.HandleBundleReparent(new BundleInfo[] { bundleVariantFolder }, concreteFolder);

      Assert.AreNotEqual(string.Empty, bundleVariantFolder.parent.m_Name.bundleName);
      Assert.AreEqual(concreteFolder, bundleVariantFolder.parent);
    }

    [Test]
    public static void HandleBundleReparent_MovesBundle_IntoCorrectVariantFolder()
    {
      var variantFolderName = "variantfolder";
      var bundleName = "bundle1";

      var bundleVariantFolderRoot = new BundleVariantFolderInfo(variantFolderName, ABModelUtil.Root);
      var bundleDataInfo = new BundleDataInfo(bundleName, ABModelUtil.Root);

      ABModelUtil.Root.AddChild(bundleVariantFolderRoot);
      ABModelUtil.Root.AddChild(bundleDataInfo);

      Model.HandleBundleReparent(new BundleInfo[] { bundleDataInfo }, bundleVariantFolderRoot);

      Assert.AreEqual(variantFolderName + "/" + bundleName, bundleDataInfo.m_Name.bundleName);
    }

    [Test]
    public static void HandleBundleDelete_Deletes_AllChildrenOfConcreteFolder()
    {
      var concreteFolder = new BundleFolderConcreteInfo("concreteFolder", ABModelUtil.Root);
      ABModelUtil.Root.AddChild(concreteFolder);

      var bundleDataInfo1 = new BundleDataInfo("bundle1", concreteFolder);
      var bundleDataInfo2 = new BundleDataInfo("bundle2", concreteFolder);
      var bundleDataInfo3 = new BundleDataInfo("bundle3", concreteFolder);

      concreteFolder.AddChild(bundleDataInfo1);
      concreteFolder.AddChild(bundleDataInfo2);
      concreteFolder.AddChild(bundleDataInfo3);

      Model.HandleBundleDelete(new BundleInfo[] { concreteFolder });

      var numberOfChildrenFieldInfo =
        typeof(BundleFolderConcreteInfo).GetField("m_Children", BindingFlags.NonPublic | BindingFlags.Instance);
      var numberOfConcreteFolderChildren =
        numberOfChildrenFieldInfo.GetValue(concreteFolder) as Dictionary<string, BundleInfo>;

      Assert.AreEqual(0, numberOfConcreteFolderChildren.Keys.Count);
    }

    [Test]
    public static void HandleBundleDelete_Deletes_BundleDataInfo()
    {
      // Account for existing asset bundles
      var numChilren = ABModelUtil.Root.GetChildList().Count;

      var bundleDataInfo1 = new BundleDataInfo("bundle1", ABModelUtil.Root);
      var bundleDataInfo2 = new BundleDataInfo("bundle2", ABModelUtil.Root);
      var bundleDataInfo3 = new BundleDataInfo("bundle3", ABModelUtil.Root);

      ABModelUtil.Root.AddChild(bundleDataInfo1);
      ABModelUtil.Root.AddChild(bundleDataInfo2);
      ABModelUtil.Root.AddChild(bundleDataInfo3);

      Model.HandleBundleDelete(new BundleInfo[] { bundleDataInfo1, bundleDataInfo2, bundleDataInfo3 });

      var numberOfChildrenFieldInfo =
        typeof(BundleFolderConcreteInfo).GetField("m_Children", BindingFlags.NonPublic | BindingFlags.Instance);
      var numberOfConcreteFolderChildren =
        numberOfChildrenFieldInfo.GetValue(ABModelUtil.Root) as Dictionary<string, BundleInfo>;

      Assert.AreEqual(numChilren, numberOfConcreteFolderChildren.Keys.Count);
    }

    [Test]
    public static void HandleBundleDelete_Deletes_VariantFolderAndChildren()
    {
      // Account for existing asset bundles
      var numChildren = ABModelUtil.Root.GetChildList().Count;

      var bundleVariantFolderRoot = new BundleVariantFolderInfo("variantFolder", ABModelUtil.Root);
      ABModelUtil.Root.AddChild(bundleVariantFolderRoot);

      var bundleVariantDataInfo1 = new BundleVariantDataInfo("variant.a", bundleVariantFolderRoot);

      var bundleVariantDataInfo2 = new BundleVariantDataInfo("variant.b", bundleVariantFolderRoot);

      var bundleVariantDataInfo3 = new BundleVariantDataInfo("variant.c", bundleVariantFolderRoot);

      bundleVariantFolderRoot.AddChild(bundleVariantDataInfo1);
      bundleVariantFolderRoot.AddChild(bundleVariantDataInfo2);
      bundleVariantFolderRoot.AddChild(bundleVariantDataInfo3);

      var numberOfChildrenFieldInfo =
        typeof(BundleFolderConcreteInfo).GetField("m_Children", BindingFlags.NonPublic | BindingFlags.Instance);
      var numberOfConcreteFolderChildren =
        numberOfChildrenFieldInfo.GetValue(ABModelUtil.Root) as Dictionary<string, BundleInfo>;

      Assert.AreEqual(numChildren + 1, numberOfConcreteFolderChildren.Keys.Count);

      Model.HandleBundleDelete(new BundleInfo[] { bundleVariantFolderRoot });

      numberOfConcreteFolderChildren =
        numberOfChildrenFieldInfo.GetValue(ABModelUtil.Root) as Dictionary<string, BundleInfo>;

      Assert.AreEqual(numChildren, numberOfConcreteFolderChildren.Keys.Count);
    }

    [Test]
    public static void HandleBundleDelete_Deletes_SingleVariantFromVariantFolder()
    {
      // Account for existing asset bundles
      var numChildren = ABModelUtil.Root.GetChildList().Count;
      var numBundles = AssetDatabase.GetAllAssetBundleNames().Length;

      var bundleVariantFolderRoot = new BundleVariantFolderInfo("variantFolder", ABModelUtil.Root);
      ABModelUtil.Root.AddChild(bundleVariantFolderRoot);

      var bundleVariantDataInfo1 = new BundleVariantDataInfo("variant1", bundleVariantFolderRoot);
      bundleVariantDataInfo1.m_Name.variant = "a";

      var bundleVariantDataInfo2 = new BundleVariantDataInfo("variant1", bundleVariantFolderRoot);
      bundleVariantDataInfo2.m_Name.variant = "b";

      var bundleVariantDataInfo3 = new BundleVariantDataInfo("variant1", bundleVariantFolderRoot);
      bundleVariantDataInfo3.m_Name.variant = "c";

      bundleVariantFolderRoot.AddChild(bundleVariantDataInfo1);
      bundleVariantFolderRoot.AddChild(bundleVariantDataInfo2);
      bundleVariantFolderRoot.AddChild(bundleVariantDataInfo3);

      var numberOfChildrenFieldInfo =
        typeof(BundleFolderConcreteInfo).GetField("m_Children", BindingFlags.NonPublic | BindingFlags.Instance);
      var numberOfConcreteFolderChildren =
        numberOfChildrenFieldInfo.GetValue(ABModelUtil.Root) as Dictionary<string, BundleInfo>;

      Assert.AreEqual(numChildren + 1, numberOfConcreteFolderChildren.Keys.Count);

      Model.HandleBundleDelete(new BundleInfo[] { bundleVariantDataInfo1 });

      numberOfConcreteFolderChildren =
        numberOfChildrenFieldInfo.GetValue(ABModelUtil.Root) as Dictionary<string, BundleInfo>;

      Assert.AreEqual(numChildren + 1, numberOfConcreteFolderChildren.Keys.Count);

      var numberOfVariantFolderChildrenFieldInfo =
        typeof(BundleVariantFolderInfo).GetField("m_Children", BindingFlags.NonPublic | BindingFlags.Instance);
      var numberOfVariantFolderChildren =
        numberOfVariantFolderChildrenFieldInfo.GetValue(bundleVariantFolderRoot) as Dictionary<string, BundleInfo>;

      Assert.AreEqual(2, numberOfVariantFolderChildren.Keys.Count);
    }

    [Test]
    public static void HandleBundleMerge_Merges_BundlesCorrectly()
    {
      // Account for existing bundles
      var numBundles = AssetDatabase.GetAllAssetBundleNames().Length;

      var bundle1Name = "bundle1";
      var bundle2Name = "bundle2";

      var bundle1DataInfo = Model.CreateEmptyBundle() as BundleDataInfo;
      Model.HandleBundleRename(new BundleTreeItem(bundle1DataInfo, 0, ABModelUtil.FakeTexture2D), bundle1Name);

      var bundle2DataInfo = Model.CreateEmptyBundle() as BundleDataInfo;
      Model.HandleBundleRename(new BundleTreeItem(bundle2DataInfo, 0, ABModelUtil.FakeTexture2D), bundle2Name);

      var listOfPrefabs = new List<string>();
      listOfPrefabs.Add(TestUtil.CreatePrefabWithBundleAndVariantName(bundle1Name, string.Empty));
      listOfPrefabs.Add(TestUtil.CreatePrefabWithBundleAndVariantName(bundle2Name, string.Empty));
      listOfPrefabs.Add(TestUtil.CreatePrefabWithBundleAndVariantName(bundle2Name, string.Empty));

      TestUtil.ExecuteCodeAndCleanupAssets(() =>
      {
        Model.HandleBundleMerge(new BundleInfo[] { bundle2DataInfo }, bundle1DataInfo);

        var bundleNames = AssetDatabase.GetAllAssetBundleNames();
        Assert.AreEqual(numBundles + 1, bundleNames.Length);
        Assert.IsTrue(bundleNames.Contains(bundle1Name));

        //Make sure every asset now has bundle1 as the bundle name
        foreach (var prefab in listOfPrefabs)
          Assert.AreEqual(bundle1Name, AssetImporter.GetAtPath(prefab).assetBundleName);
      }, listOfPrefabs);
    }

    [Test]
    public static void HandleBundleMerge_Merges_BundlesWithChildrenCorrectly()
    {
      // Account for existing bundles
      var numBundles = AssetDatabase.GetAllAssetBundleNames().Length;

      var folderName = "folder";
      var bundle1Name = "bundle1";
      var bundle2Name = folderName + "/bundle2";

      var concrete = new BundleFolderConcreteInfo(folderName, ABModelUtil.Root);
      var bundle1DataInfo = new BundleDataInfo(bundle1Name, ABModelUtil.Root);
      var bundle2DataInfo = new BundleDataInfo(bundle2Name, concrete);
      concrete.AddChild(bundle2DataInfo);

      var listOfPrefabs = new List<string>();
      listOfPrefabs.Add(TestUtil.CreatePrefabWithBundleAndVariantName(bundle1Name, string.Empty));
      listOfPrefabs.Add(TestUtil.CreatePrefabWithBundleAndVariantName(bundle2Name, string.Empty));
      listOfPrefabs.Add(TestUtil.CreatePrefabWithBundleAndVariantName(bundle2Name, string.Empty));

      TestUtil.ExecuteCodeAndCleanupAssets(() =>
      {
        Model.HandleBundleMerge(new BundleInfo[] { bundle1DataInfo }, bundle2DataInfo);

        var bundleNames = AssetDatabase.GetAllAssetBundleNames();
        Assert.AreEqual(numBundles + 1, bundleNames.Length, GetAllElementsAsString(bundleNames));
        Assert.IsTrue(bundleNames.Contains(bundle2Name));

        //Make sure every asset now has bundle1 as the bundle name
        foreach (var prefab in listOfPrefabs)
          Assert.AreEqual(bundle2Name, AssetImporter.GetAtPath(prefab).assetBundleName);
      }, listOfPrefabs);
    }

    [Test]
    public static void HandleConvertToVariant_Converts_BundlesToVariant()
    {
      BundleInfo dataInfo = new BundleDataInfo("folder", ABModelUtil.Root);
      dataInfo = Model.HandleConvertToVariant((BundleDataInfo)dataInfo);
      Assert.AreEqual(typeof(BundleVariantDataInfo), dataInfo.GetType());
    }

    [Test]
    public static void HandleDedupeBundles_MovesDuplicatedAssets_ToNewBundle()
    {
      var bundle1PrefabInstanceName = "Bundle1Prefab";
      var bundle2PrefabInstanceName = "Bundle2Prefab";

      var bundle1Name = "bundle1";
      var bundle2Name = "bundle2";

      var listOfAssets = new List<string>();
      listOfAssets.Add(TestUtil.CreatePrefabWithBundleAndVariantName(bundle1Name, "", bundle1PrefabInstanceName));
      listOfAssets.Add(TestUtil.CreatePrefabWithBundleAndVariantName(bundle2Name, "", bundle2PrefabInstanceName));

      var bundle1DataInfo = new BundleDataInfo(bundle1Name, ABModelUtil.Root);
      var bundle2DataInfo = new BundleDataInfo(bundle2Name, ABModelUtil.Root);

      ABModelUtil.Root.AddChild(bundle1DataInfo);
      ABModelUtil.Root.AddChild(bundle2DataInfo);

      bundle1DataInfo.RefreshAssetList();
      bundle2DataInfo.RefreshAssetList();

      //Need a material with no assigned bundle so it'll be pulled into both bundles
      var materialPath = "Assets/material.mat";
      var mat = new Material(Shader.Find("Diffuse"));
      AssetDatabase.CreateAsset(mat, materialPath);
      listOfAssets.Add(materialPath);
      //

      Model.Refresh();

      TestUtil.ExecuteCodeAndCleanupAssets(() =>
      {
        AddMaterialsToMultipleObjects(new[] { bundle1PrefabInstanceName, bundle2PrefabInstanceName }, listOfAssets, mat);
        Model.HandleDedupeBundles(new BundleInfo[] { bundle1DataInfo, bundle2DataInfo }, false);
        //This checks to make sure that a newbundle was automatically created since we dont' set this up anywhere else.
        Assert.IsTrue(AssetDatabase.GetAllAssetBundleNames().Contains("newbundle"));
      }, listOfAssets);
    }

    private static int GetBundleRootFolderChildCount()
    {
      var childList = ABModelUtil.Root.GetChildList();
      return childList.Count;
    }

    private static void AddMaterialsToMultipleObjects(IEnumerable<string> parentNames, IEnumerable<string> paths,
      Material mat)
    {
      for (var i = 0; i < parentNames.Count(); i++)
      {
        var p = GameObject.Find(parentNames.ElementAt(i));
        p.GetComponent<Renderer>().material = mat;

        PrefabUtility.ReplacePrefab(p, AssetDatabase.LoadMainAssetAtPath(paths.ElementAt(i)));
      }
    }

    private static string GetAllElementsAsString(IEnumerable<string> list)
    {
      var returnString = string.Empty;
      foreach (var i in list) returnString += i + ", ";

      return returnString;
    }
  }
}