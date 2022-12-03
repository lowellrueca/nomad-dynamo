import clr

clr.AddReference('RevitAPI')

from Autodesk.Revit.DB import *


def gen_type_marks(elem_types, add_key=None):
    """
    Generates type mark values for the element types.

    :param elem_types: the list of element types.
    :param add_key: an additional key to be used.
    :returns: list of generated type marks
    """
    get_key = lambda e: e.LookupParameter('Set Key').AsString()
    gen_num = lambda e: elem_types.IndexOf(e) + 1
    gen_type_mark = lambda e: str.format('{}-{}', get_key(e), gen_num(e))
    gen_type_mark_w_add_key = lambda e: str.format('{}-{}-{}', get_key(e), add_key, gen_num(e))

    type_marks = []

    if add_key is not None:
        type_marks = map(lambda e: gen_type_mark_w_add_key(e), elem_types)

    if add_key is None:
        type_marks = map(lambda e: gen_type_mark(e), elem_types)

    return type_marks

def set_type_marks(elem_types, type_marks):
    """
    Sets the type mark value for each element types

    :param elem_types: list of element types
    :param type_marks: list of type marks
    """
    bip = BuiltInParameter.ALL_MODEL_TYPE_MARK
    map(lambda e, d: e.get_Parameter(bip).Set(d), elem_types, type_marks)

def set_descriptions(elem_types, descriptions):
    """
    Sets the description value for each element types

    :param elem_types: list of element types
    :param description: list of description
    """
    bip = BuiltInParameter.ALL_MODEL_DESCRIPTION
    map(lambda e, d: e.get_Parameter(bip).Set(d), elem_types, descriptions)

def set_parameter(elements, param_name, param_val):
    """
    Sets the value for defined parameter name

    :param elements: list of elements or element types
    :param param_name: name of parameter
    :param param_val: value of the parameter to set
    """
    map(lambda e: e.LookupParameter(param_name).Set(param_val), elements)

def get_string(elem, param_name):
    res = ''
    param = elem.LookupParameter(param_name)

    if param.Definition.ParameterType.Equals(ParameterType.Text):
        res = elem.LookupParameter(param_name).AsString()
    
    return res

def get_as_value_string(elem, param_name):
    res = ''
    param = elem.LookupParameter(param_name)

    if param.Definition.ParameterType.Equals(ParameterType.Text):
        res = elem.LookupParameter(param_name).AsValueString()
    
    return res

def get_integer(elem, param_name):
    res = None
    param = elem.LookupParameter(param_name)

    if param.Definition.ParameterType.Equals(ParameterType.Integer):
        res = elem.LookupParameter(param_name).AsInteger()

    return res

def get_bool(elem, param_name):
    res = None
    param = elem.LookupParameter(param_name)

    if param.Definition.ParameterType.Equals(ParameterType.YesNo):
        res = elem.LookupParameter(param_name).AsInteger()

    return res

def get_length(elem, param_name):
    res = None
    param = elem.LookupParameter(param_name)

    dut_ft = DisplayUnitType.DUT_DECIMAL_FEET
    dut_mm = DisplayUnitType.DUT_MILLIMETERS

    convert_ft_to_mm = lambda el, pn: UnitUtils.Convert(el.LookupParameter(pn).AsDouble(), dut_ft, dut_mm)

    if param.Definition.ParameterType.Equals(ParameterType.Length):
        res = convert_ft_to_mm(elem, param_name)

    return res

def get_area(elem, param_name):
    res = None
    param = elem.LookupParameter(param_name)

    dut_sqft = DisplayUnitType.DUT_SQUARE_FEET
    dut_sqm = DisplayUnitType.DUT_SQUARE_METERS

    convert_sqft_to_sqm = lambda el, pn: UnitUtils.Convert(el.LookupParameter(pn).AsDouble(), dut_sqft, dut_sqm)

    if param.Definition.ParameterType.Equals(ParameterType.Length):
        res = convert_sqft_to_sqm(elem, param_name)

    return res
