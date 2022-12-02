"""
This module handles the revit's built in parameters
"""
import clr

clr.AddReference('RevitAPI')

from Autodesk.Revit.DB import BuiltInParameter


bip_type_mark = BuiltInParameter.ALL_MODEL_TYPE_MARK
bip_description = BuiltInParameter.ALL_MODEL_DESCRIPTION

def set_description(elem_types, descriptions):
    """
    Sets the values for Type Mark parameters of the element types

    :param elem_types: the list of element types
    :param type_marks: the list of type marks for each element types
    :returns: the retrieved type mark values set for the element types
    """
    bip = BuiltInParameter.ALL_MODEL_DESCRIPTION
    map(lambda et, d: et.get_Parameter(bip).Set(d), elem_types, descriptions)

    return map(lambda et: et.get_Parameter(bip).AsString())

def set_type_marks(elem_types, add_key=None):
    """
    Sets the values for Type Mark parameters of the element types

    :param elem_types: the list of element types
    :param type_marks: the list of type marks for each element types
    :returns: the retrieved type mark values set for the element types
    """
    get_key = lambda e: e.LookupParameter('Set Key').AsString()
    gen_num = lambda e: elem_types.IndexOf(e) + 1
    gen_type_mark = lambda e: str.format('{}-{}', get_key(e), gen_num(e))
    gen_type_mark_w_add_key = lambda e: str.format('{}-{}-{}', get_key(e), add_key, gen_num(e))

    if add_key is not None:
        map(lambda e: e.get_Parameter(bip_type_mark).Set(gen_type_mark_w_add_key(e)), elem_types)

    if add_key is None:
        map(lambda e: e.get_Parameter(bip_type_mark).Set(gen_type_mark(e)), elem_types)
