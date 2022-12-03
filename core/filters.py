import clr

from params import get_string


def filter_elems_by_param_val_string(elements, param_name, starts_with = None, contains = None):
    res = []
    if starts_with is not None:
        res = filter(lambda e: get_string(e, param_name).StartsWith(starts_with), elements)
    
    if contains is not None:
        res = filter(lambda e: get_string(e, param_name).Contains(contains), elements)

    return res
