import sys

sys.path.append('C:/ProgramData/Dynamo/Dynamo Revit/1.3/definitions/teascripts-dynamo')

from core import *

def sort_hss(family, base, depth, thickness):
    """
    It sorts the element types of a hollow steel section shape "HSS" family,
    and it creates seperate list for square hollow section "SHS" types
    and rectangular hollow section "RHS" types.

    :param family: a family class of an HSS shape object.
    :returns: list of element types of shs and rhs.
    """
    element_types = element_types_of_family(family)
    sz = {'b': base, 'd': depth, 't': thickness}
    sort_by_size = lambda e: (convert_ft_to_mm(e, sz['b']), convert_ft_to_mm(e, sz['d']), convert_ft_to_mm(e, sz['t']))

    # sort shs types
    shs_types = filter(lambda e: convert_ft_to_mm(e, sz['b']) == convert_ft_to_mm(e, sz['d']), element_types)
    shs_types_by_sz = sorted(shs_types, key=lambda e: sort_by_size(e)) # sort shs types by sizes

    # sort rhs types
    rhs_types = filter(lambda e: convert_ft_to_mm(e, sz['b']) != convert_ft_to_mm(e, sz['d']), element_types)
    rhs_types_by_sz = sorted(rhs_types, key=lambda e: sort_by_size(e))

    return  shs_types_by_sz, rhs_types_by_sz
