using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stringContact
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> templateStrings = new List<string>()
            {
                "BLLOP_AddOneRecord_Failed",
"BLLOP_AddOneRecordWithInputData_Failed",
"BLLOP_AnalysisListPackageInfo_Failed",
"BLLOP_AnalysisPackageInfo_Failed",
"BLLOP_AppendMultipleRecords_Failed",
"BLLOP_ApplyMVO_Failed",
"BLLOP_AssignRecordsFromServerPool_Failed",
"BLLOP_BeginConstructDocument_Failed",
"BLLOP_BeginEditDocument_Failed",
"BLLOP_BLDocumentToEMRadio_Failed",
"BLLOP_CaculateMismatch_Failed",
"BLLOP_CanAddOneRecord_Failed",
"BLLOP_CanAddOneRecordWithInputType_Failed",
"BLLOP_CanAssignRecordsFromServerPool_Failed",
"BLLOP_CanDeleteOneRecord_Failed",
"BLLOP_CanMakeCopyAndAppendSet_Failed",
"BLLOP_CanSaveDoc_Failed",
"BLLOP_CheckIsSupportedCPVersion_Failed",
"BLLOP_CheckModelNumberInAliasMap_Failed",
"BLLOP_Compile_Failed",
"BLLOP_ConvertPCIFieldsToOptions_Failed",
"BLLOP_CreateDefaultConfigurationStructure_Failed",
"BLLOP_CreateDefaultFullDocument_Failed",
"BLLOP_Decompile_Failed",
"BLLOP_DeleteOneRecord_Failed",
"BLLOP_DeleteUnUsedGlobalRecords_Failed",
"BLLOP_DisposeDocument_Failed",
"BLLOP_EMRadioToBLDocument_Failed",
"BLLOP_EnterPartialValidationMode_Failed",
"BLLOP_EvalutationMinimumOptionPerRecord_Failed",
"BLLOP_ExitPartialValiationMode_Failed",
"BLLOP_ExportDBNodeFromBLNode_Failed",
"BLLOP_ExportDBNodeFromDocument_Failed",
"BLLOP_ExportPBA_Failed",
"BLLOP_ExtractFieldValueFromPBA_Failed",
"BLLOP_FillMissedConfigurationRecords_Failed",
"BLLOP_FillMissedConfigurationRecords_Failed",
"BLLOP_FindAndcheckPCIFieldValue_Failed",
"BLLOP_FindRecordBySetName_Failed",
"BLLOP_GenerateDefaultEMRadioByMVO_Failed",
"BLLOP_GenerateDefaultRadioBLNodeRecSet_Failed",
"BLLOP_GenerateMVOEntryFromPBA_Failed",
"BLLOP_GenerateNewEMRadioByAnotherRadio_Failed",
"BLLOP_GetASTROOptionsCollection_Failed",
"BLLOP_GetAvailableOptionsCollection_Failed",
"BLLOP_GetBaselineCPVersion_Failed",
"BLLOP_GetBusinessModelOfConfigurationFromRadio_Failed",
"BLLOP_GetConfigurationOptions_Failed",
"BLLOP_GetDataBlockTypeList_Failed",
"BLLOP_GetDependentSetList_Failed",
"BLLOP_GetDynamicBlockSizeList_Failed",
"BLLOP_GetEmbeddedSetNameList_Failed",
"BLLOP_GetFieldValueFromBlockData_Failed",
"BLLOP_GetFPSEnabledOptions_Failed",
"BLLOP_GetFPSFieldsName_Failed",
"BLLOP_GetFullFilledDocument_Failed",
"BLLOP_GetMessageForMismatchedOptions_Failed",
"BLLOP_GetModelNumberList_Failed",
"BLLOP_GetMTFData_Failed",
"BLLOP_GetMVOEntryFromBLDocument_Failed",
"BLLOP_GetMVOEntryFromRadio_Failed",
"BLLOP_GetNewNumThresOfRecSet_Failed",
"BLLOP_GetOptionsFieldName_Failed",
"BLLOP_GetOriginalRefset_Failed",
"BLLOP_GetPCIFieldsByFirmware_Failed",
"BLLOP_GetProductFamilyVersionsMap_Failed",
"BLLOP_GetProductIdentifierList_Failed",
"BLLOP_GetRecSetNoMvoMaxNumber_Failed",
"BLLOP_GetRecSetNoMvoMaxNumber_Failed",
"BLLOP_GetRefFieldOriginalRefSetPaths_Failed",
"BLLOP_GetSaveDataFromDoc_Failed",
"BLLOP_GetSetNatureTypeByRecsetSetName_Failed",
"BLLOP_GetSetNatureTypeByRecsetSetName_Failed",
"BLLOP_GetSetProductFamily_Failed",
"BLLOP_GetSetTypeByRecsetSetName_Failed",
"BLLOP_GetSupportedVersions_Failed",
"BLLOP_GetSyncDataOfConfiguration_Failed",
"BLLOP_GetTypesOfSet_Failed",
"BLLOP_GetTypesOfTopSetInSetMode_Failed",
"BLLOP_HasImpactConfiguration_Failed",
"BLLOP_ImportPBA_Failed",
"BLLOP_IsDocumentReadyForEdit_Failed",
"BLLOP_IsNewDocument_Failed",
"BLLOP_IsNewRecord_Failed",
"BLLOP_IsReferFieldNeedLoadSet_Failed",
"BLLOP_IsSetTypeNeedSync_Failed",
"BLLOP_IsWeakTypeSetForTopSetInSetMode_Failed",
"BLLOP_LoadDepSetUponSetMode_Failed",
"BLLOP_LoadRadioIntoDocument_Failed",
"BLLOP_MakeCopyAndAppendEmbeddedSet_Failed",
"BLLOP_ModifyTargetPbaObject_forRestoreJob_Failed",
"BLLOP_NewOneTopRecordInSetMode_Failed",
"BLLOP_NewOneTopRecordInSetModeWithInput_Failed",
"BLLOP_OpenConfiguration_Failed",
"BLLOP_OpenConfigurationSet_Failed",
"BLLOP_PopulateValidationFields_Failed",
"BLLOP_ReadRadio_Failed",
"BLLOP_ResetFieldByMVO_Failed",
"BLLOP_RetreiveSetsList_Failed",
"BLLOP_RetrieveMandantorySets_Failed",
"BLLOP_RetrieveMandantorySets_Failed",
"BLLOP_SelectRefRecordFromServerPool_Failed",
"BLLOP_SetApplicationLicenses_Failed",
"BLLOP_SetConstaintLevel_Failed",
"BLLOP_SetVirtualRadioOptionsFields_PCI_Failed",
"BLLOP_SpellCheck_Failed",
"BLLOP_TrySetFieldValueByStringValue_Failed",
"BLLOP_UpdateTimeStampAndSetName_Failed",
"BLLOP_ValidateModelNumberAndFlashcode_Failed",
"BLLOP_WriteRadio_Failed",

            };

            StringBuilder sb = new StringBuilder();
            foreach(string strItem in templateStrings)
            {
                sb.AppendFormat(@"<data name=""{0}"" xml:space=""preserve"">"+"\n    <value></value>\n  </data>\n", strItem);
                
            }

            System.Console.WriteLine(sb.ToString());
        }
    }
}
