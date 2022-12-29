using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class OmImpressionData
{

    public const string KEY_IMPRESSION_ID = "impression_id";
    public const string KEY_INSTANCE_ID = "instance_id";
    public const string KEY_INSTANCE_NAME = "instance_name";
    public const string KEY_INSTANCE_PRIORITY = "instance_priority";
    public const string KEY_AD_NETWORK_ID = "ad_network_id";
    public const string KEY_AD_NETWORK_NAME = "ad_network_name";
    public const string KEY_AD_NETWORK_UNIT_ID = "ad_network_unit_id";
    public const string KEY_MEDIATION_RULE_ID = "mediation_rule_id";
    public const string KEY_MEDIATION_RULE_NAME = "mediation_rule_name";
    public const string KEY_MEDIATION_RULE_TYPE = "mediation_rule_type";
    public const string KEY_MEDIATION_RULE_PRIORITY = "mediation_rule_priority";
    public const string KEY_PLACEMENT_ID = "placement_id";
    public const string KEY_PLACEMENT_NAME = "placement_name";
    public const string KEY_PLACEMENT_AD_TYPE = "placement_ad_type";
    public const string KEY_SCENE_NAME = "scene_name";
    public const string KEY_CURRENCY = "currency";
    public const string KEY_REVENUE = "revenue";
    public const string KEY_PRECISION = "precision";
    public const string KEY_AB_GROUP = "ab_group";
    public const string KEY_LIFETIME_VALUE = "lifetime_value";

    public readonly string impressionId;
    public readonly string instanceId;
    public readonly string instanceName;
    public readonly int? instancePriority;
    public readonly string adNetworkId;
    public readonly string adNetworkName;
    public readonly string adNetworkUnitId;
    public readonly string mediationRuleId;
    public readonly string mediationRuleName;
    public readonly string mediationRuleType;
    public readonly int? mediationRulePriority;
    public readonly string placementId;
    public readonly string placementName;
    public readonly string placementAdType;
    public readonly string sceneName;
    public readonly string currency;
    public readonly double? revenue;
    public readonly string precision;
    public readonly string abGroup;
    public readonly double? lifetimeValue;
    public readonly string allData;

    public OmImpressionData(string json)
    {
        if (json != null)
        {
            try
            {
                object obj;
                double parsedDouble;
                int parsedInt;
                allData = json;
                // Retrieve a CultureInfo object.
                CultureInfo invCulture = CultureInfo.InvariantCulture;
                Dictionary<string, object> jsonDic = OmJSON.Json.Deserialize(json) as Dictionary<string, object>;
                if (jsonDic.TryGetValue(KEY_IMPRESSION_ID, out obj) && obj != null)
                {
                    impressionId = obj.ToString();
                }
                if (jsonDic.TryGetValue(KEY_INSTANCE_ID, out obj) && obj != null)
                {
                    instanceId = obj.ToString();
                }
                if (jsonDic.TryGetValue(KEY_INSTANCE_NAME, out obj) && obj != null)
                {
                    instanceName = obj.ToString();
                }
                if (jsonDic.TryGetValue(KEY_INSTANCE_PRIORITY, out obj) && obj != null && int.TryParse(string.Format(invCulture, "{0}", obj), NumberStyles.Any, invCulture, out parsedInt))
                {
                    instancePriority = parsedInt;
                }
                if (jsonDic.TryGetValue(KEY_AD_NETWORK_ID, out obj) && obj != null)
                {
                    adNetworkId = obj.ToString();
                }
                if (jsonDic.TryGetValue(KEY_AD_NETWORK_NAME, out obj) && obj != null)
                {
                    adNetworkName = obj.ToString();
                }
                if (jsonDic.TryGetValue(KEY_AD_NETWORK_UNIT_ID, out obj) && obj != null)
                {
                    adNetworkUnitId = obj.ToString();
                }
                if (jsonDic.TryGetValue(KEY_MEDIATION_RULE_ID, out obj) && obj != null)
                {
                    mediationRuleId = obj.ToString();
                }
                if (jsonDic.TryGetValue(KEY_MEDIATION_RULE_NAME, out obj) && obj != null)
                {
                    mediationRuleName = obj.ToString();
                }
                if (jsonDic.TryGetValue(KEY_MEDIATION_RULE_TYPE, out obj) && obj != null)
                {
                    mediationRuleType = obj.ToString();
                }
                if (jsonDic.TryGetValue(KEY_MEDIATION_RULE_PRIORITY, out obj) && obj != null && int.TryParse(string.Format(invCulture, "{0}", obj), NumberStyles.Any, invCulture, out parsedInt))
                {
                    mediationRulePriority = parsedInt;
                }
                if (jsonDic.TryGetValue(KEY_PLACEMENT_ID, out obj) && obj != null)
                {
                    placementId = obj.ToString();
                }
                if (jsonDic.TryGetValue(KEY_PLACEMENT_NAME, out obj) && obj != null)
                {
                    placementName = obj.ToString();
                }
                if (jsonDic.TryGetValue(KEY_PLACEMENT_AD_TYPE, out obj) && obj != null)
                {
                    placementAdType = obj.ToString();
                }
                if (jsonDic.TryGetValue(KEY_SCENE_NAME, out obj) && obj != null)
                {
                    sceneName = obj.ToString();
                }
                if (jsonDic.TryGetValue(KEY_CURRENCY, out obj) && obj != null)
                {
                    currency = obj.ToString();
                }
                if (jsonDic.TryGetValue(KEY_REVENUE, out obj) && obj != null && double.TryParse(string.Format(invCulture, "{0}", obj), NumberStyles.Any, invCulture, out parsedDouble))
                {
                    revenue = parsedDouble;
                }
                if (jsonDic.TryGetValue(KEY_PRECISION, out obj) && obj != null)
                {
                    precision = obj.ToString();
                }
                if (jsonDic.TryGetValue(KEY_AB_GROUP, out obj) && obj != null)
                {
                    abGroup = obj.ToString();
                }

                if (jsonDic.TryGetValue(KEY_LIFETIME_VALUE, out obj) && obj != null && double.TryParse(string.Format(invCulture,"{0}",obj), NumberStyles.Any, invCulture, out parsedDouble))
                {
                    lifetimeValue = parsedDouble;
                }
            }
            catch (Exception ex)
            {
                Debug.Log("error parsing impression " + ex.ToString());
            }

        }
    }

    public override string ToString()
    {
        return "OmImpressionData{" +
                "impressionId='" + impressionId + '\'' +
                ", instanceId='" + instanceId + '\'' +
                ", instanceName='" + instanceName + '\'' +
                ", instancePriority=" + instancePriority +
                ", adNetworkId='" + adNetworkId + '\'' +
                ", adNetworkName='" + adNetworkName + '\'' +
                ", adNetworkUnitId='" + adNetworkUnitId + '\'' +
                ", mediationRuleId='" + mediationRuleId + '\'' +
                ", mediationRuleName='" + mediationRuleName + '\'' +
                ", mediationRuleType=" + mediationRuleType +
                ", mediationRulePriority=" + mediationRulePriority +
                ", placementId='" + placementId + '\'' +
                ", placementName='" + placementName + '\'' +
                ", placementAdType='" + placementAdType + '\'' +
                ", sceneName='" + sceneName + '\'' +
                ", currency='" + currency + '\'' +
                ", revenue=" + revenue +
                ", precision='" + precision + '\'' +
                ", abGroup='" + abGroup + '\'' +
                ", lifetimeValue=" + lifetimeValue +
                '}';
    }
}