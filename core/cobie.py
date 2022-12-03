import clr

clr.AddReference('RevitAPI')

from Autodesk.Revit.DB import *
from utils import created_on


def set_cobie(elements, param_name, param_val):
    """
    Sets the value for COBie or COBie.Type parameter
    to be able to include in COBie listing.

    :param elements: list of elements or element types
    :param param_val: the integer value to set
    """
    param_name_comp = 'COBie'
    param_name_type = 'COBie.Type'
    
    if param_name is param_name_comp:
        map(lambda e: e.LookupParameter(param_name_comp).Set(param_val), elements)
        
    if param_name is param_name_type:
        map(lambda e: e.LookupParameter(param_name_type).Set(param_val), elements)

def set_cobie_datetime(elements, param_name):
    """
    Sets the datetime for COBie.Type.CreatedOn

    :param elements: list of elements or element types
    :param param_name: parameter name to set datetime
    """
    param_name_type = 'COBie.Type.CreatedOn'
    param_name_comp = 'COBie.CreatedOn'
    if param_name is param_name_type:
        map(lambda e: e.LookupParameter(param_name_type).Set(created_on()), elements)

    if param_name is param_name_comp:
        map(lambda e: e.LookupParameter(param_name_comp).Set(created_on()), elements)

def set_cobie_type_category(element_types):
    """
    Sets the omniclass code and title as value for COBie.Type.Category parameter

    :param element_types: list of element types
    """
    param_name = 'COBie.Type.Category'
    bip_oc = BuiltInParameter.OMNICLASS_CODE
    bip_od = BuiltInParameter.OMNICLASS_DESCRIPTION

    omniclass_numbers = map(lambda e: e.get_Parameter(bip_oc).AsString(), element_types)
    omniclass_titles = map(lambda e: e.get_Parameter(bip_od).AsString(), element_types)
    
    categories = map(lambda n, t: str.format('{0}: {1}', n, t), omniclass_numbers, omniclass_titles)

    map(lambda e, c: e.LookupParameter(param_name).Set(c), element_types, categories)

def set_cobie_type_description(element_types):
    """
    Sets the value for COBie.Type.Description parameter

    :param element_types: list of element types
    """
    param_name = 'COBie.Type.Description'
    bip = BuiltInParameter.ALL_MODEL_DESCRIPTION
    
    descriptions = map(lambda e: e.get_Parameter(bip).AsString(), element_types)
    map(lambda e, d: e.LookupParameter(param_name).Set(d), element_types, descriptions)

def set_cobie_type_name(element_types):
    """
    It retrieves the element name and set on the COBie.Type.Name parameter

    :param element_types: list of element types
    """
    param_name = 'COBie.Type.Name'
    names = map(lambda e: Element.Name.GetValue(e), element_types)
    map(lambda e, n: e.LookupParameter(param_name).Set(n), element_types, names)
