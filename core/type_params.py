def set_description(elem_types, descriptions):
    """
    Sets the values for Type Mark parameters of the element types

    :param elem_types: the list of element types
    :param type_marks: the list of type marks for each element types
    :returns: the retrieved type mark values set for the element types
    """
    param_name = 'Description'
    map(lambda et, d: et.LookupParameter(param_name).Set(d), elem_types, descriptions)

    return map(lambda et: et.LookupParameter(param_name).AsString())

def generate_type_marks(elem_types, key):
    """
    Generate type marks for the element types

    :param elem_type: the list of element types
    :param key: the key used as prefix before the generated number
    :returns: generated type marks for each element types
    """
    generate_nums = lambda e: elem_types.IndexOf(e) + 1
    return map(lambda et: str.format('{0}-{1}', key, generate_nums(et)), elem_types)

def set_type_marks(elem_types, type_marks):
    """
    Sets the values for Type Mark parameters of the element types

    :param elem_types: the list of element types
    :param type_marks: the list of type marks for each element types
    :returns: the retrieved type mark values set for the element types
    """
    param_name = 'Type Mark'
    map(lambda et, tm: et.LookupParameter(param_name).Set(tm), elem_types, type_marks)

    return map(lambda et: et.LookupParameter(param_name).AsString())
