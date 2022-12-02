import clr

clr.AddReference('RevitAPI')

from Autodesk.Revit.DB import *
from utils import created_on


"""
Sets the value for COBie.Type parameter
to include or exclude from COBie list.

:param et: the element type
:param pv: the integer value to set
"""
set_cobie_type = lambda et, pv: et.LookupParameter('COBie.Type').Set(pv)

"""
Sets the value for COBie.Type.AssetType parameter

:param et: the element type
:param pv: the string value to set
"""
set_cobie_type_asset_type = lambda et, pv: et.LookupParameter('COBie.Type.AssetType').Set(pv)

"""
Sets the value for COBie.CreatedBy parameter.

:param et: the element type
:param pv: the string, or an email value to set
"""
set_cobie_type_created_by = lambda et, pv: et.LookupParameter('COBie.CreatedBy').Set(pv)

"""
Sets the date for COBie.Type.CreatedOn

:param et: the element type
"""
set_cobie_type_created_on = lambda et: et.LookupParameter('COBie.Type.CreatedOn').Set(created_on())

def set_cobie_type_category(et):
    """
    Sets the value for COBie.Type.Category parameter

    :param et: the element type
    """
    bip_oc = BuiltInParameter.OMNICLASS_CODE
    bip_od = BuiltInParameter.OMNICLASS_DESCRIPTION

    omniclass_number = et.get_Parameter(bip_oc).AsString()
    omniclass_title = et.get_Parameter(bip_od).AsString()
    
    category = str.format('{0}: {1}', omniclass_number, omniclass_title)

    param_name = 'COBie.Type.Category'
    et.LookupParameter(param_name).Set(category)

def set_cobie_type_description(et):
    """
    Sets the value for COBie.Type.Description parameter

    :param et: the element type
    """
    param_name = 'COBie.Type.Description'
    bip = BuiltInParameter.ALL_MODEL_DESCRIPTION
    
    description = et.get_Parameter(bip).AsString()
    et.LookupParameter(param_name).Set(description)

def set_type_marks(et):
    """
    Sets the value for COBie.Type.Name parameter

    :param et: the element type
    """
    element_type_name = Element.Name.GetValue(et)
    et.LookupParameter('COBie.Type.Name').Set(element_type_name)
