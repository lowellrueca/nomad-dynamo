import clr

clr.AddReference('RevitAPI')

from Autodesk.Revit.DB import Element
from utils import created_on
from datetime import datetime


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
    omniclass_number = et.LookupParameter('OmniClass Number').AsValueString()
    omniclass_title = et.LookupParameter('OmniClass Title').AsValueString()
    category = str.format('{0}: {1}', omniclass_number, omniclass_title)
    et.LookupParameter('COBie.Type.Category').Set(category)

def set_cobie_type_description(et):
    """
    Sets the value for COBie.Type.Description parameter

    :param et: the element type
    """
    description = et.LookupParameter('Description').AsString()
    et.LookupParameter('COBie.Type.Description').Set(description)

def set_cobie_type_name(et):
    """
    Sets the value for COBie.Type.Name parameter

    :param et: the element type
    """
    element_type_name = Element.Name.GetValue(et)
    et.LookupParameter('COBie.Type.Name').Set(element_type_name)
