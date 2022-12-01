import clr

clr.AddReference('RevitAPI')

from datetime import datetime
from Autodesk.Revit.DB import DisplayUnitType, UnitUtils


dut_ft = DisplayUnitType.DUT_DECIMAL_FEET
dut_mm = DisplayUnitType.DUT_MILLIMETERS

"""
Converts dimension from decimal feet to mm

:param el: the element or the element type
:param pn: the parameter name
"""
convert_ft_to_mm = lambda el, pn: UnitUtils.Convert(el.LookupParameter(pn).AsDouble(), dut_ft, dut_mm)

def created_on():
    """
    Gets the current date and time with COBie format

    :returns: string formatted date and time in the form of 2000-01-01T08:10:10
    """
    dt_now = datetime.now()
    year = dt_now.year
    month = dt_now.month
    day = dt_now.day

    yy_mm_dd = str.format('{0}-{1}-{2}', year, month, day)

    hour = dt_now.hour
    minute = dt_now.minute
    second = dt_now.second

    hh_mm_ss = str.format('T{0}:{1}:{2}', hour, minute, second)

    date = str.format('{0}{1}', yy_mm_dd, hh_mm_ss)

    return date

def get_size(et, *args):
    """
    Tries to retrieve the number alues from the parameter names

    :param et: the element type
    :param args: parameter names
    :return: size in string
    """
    res = ''

    if len(args) == 3:
        res = str.format('{} x {} x {}', convert_ft_to_mm(et, args[0]), convert_ft_to_mm(et, args[1]), convert_ft_to_mm(et, args[2]))

    else:
        res = str.format('{} x {}', convert_ft_to_mm(et, args[0]), convert_ft_to_mm(et, args[1]))

    return res
