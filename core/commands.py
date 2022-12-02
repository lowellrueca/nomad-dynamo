import clr

clr.AddReference('RevitAPI')

from Autodesk.Revit.DB import *


def set_element_name(elements):
    """
    Set new name for the elements or element types
    by type mark as prefix followed by description.

    :param elements: list of elements or element types
    """
    bip_type_mark = BuiltInParameter.ALL_MODEL_TYPE_MARK
    bip_type_description = BuiltInParameter.ALL_MODEL_DESCRIPTION

    type_marks = map(lambda et: et.get_Parameter(bip_type_mark).AsString(), elements)
    descriptions = map(lambda et: et.get_Parameter(bip_type_description).AsString(), elements)
    
    set_names = map(lambda tmrk, desc: str.format('{0} - {1}', tmrk, desc), type_marks, descriptions)

    map(lambda et, sn: Element.Name.SetValue(et, sn), elements, set_names)
