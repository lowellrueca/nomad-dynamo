"""
This module handles collecting different elements in the document.
"""

import clr

clr.AddReference('RevitAPI')
clr.AddReference('RevitServices')

from System.Collections.Generic import List
from Autodesk.Revit.DB import *


def get_families_by_category(doc, category):
    """
    Retrieves the families by category

    :param category: the family's category
    :returns: families of the category
    """
    families = FilteredElementCollector(doc).OfClass(Family).WhereElementIsNotElementType()
    return filter(lambda f: f.FamilyCategoryId.Equals(category.Id) and f.IsInPlace.Equals(False), families)

def get_element_types_by_category(doc, category):
    """
    Retrieves the element types by category

    :param category: the element type's category
    :returns: element types of the category
    """
    element_types = FilteredElementCollector(doc).OfCategoryId(category.Id).WhereElementIsElementType()
    return map(lambda et: et, element_types)

def get_elements_by_category(doc, category):
    """
    Retrieves the elements by category

    :param category: the element's category
    :returns: elements of the category
    """
    elements = FilteredElementCollector(doc).OfCategoryId(category.Id).WhereElementIsNotElementType().ToElements()
    return elements

def get_element_types_of_family(family):
    """
    Retrieves the element types of the family

    :param family: the family element
    :returns: list of element types of the family
    """
    cat = family.FamilyCategory
    family_types = get_element_types_by_category(cat)
    return filter(lambda ft: ft.Family.Id.Equals(family.Id), family_types)

def get_element_types_of_elements(elements):
    """
    Retrieves the element types of the elements

    :param elements: the list of elements
    :returns: the list of element type of the elements
    """
    element_ids = List[ElementId](map(lambda element: element.GetTypeId(), elements))
    return FilteredElementCollector(__document__, element_ids).WhereElementIsElementType().ToElements()

def get_elements_of_element_type(doc, element_type):
    """
    Retrieves the elements of an element type

    :param element_type: the element type
    :returns: a list of elements of an element type
    """
    elements = FilteredElementCollector(doc).OfCategoryId(element_type.Category.Id).WhereElementIsNotElementType().ToElements()
    return filter(lambda element: element.GetTypeId().Equals(element_type.Id), elements)

def set_element_types_name(element_types):
    """
    Set new name for the element types
    by type mark as prefix followed by description.

    :param elements: list of elements or element types
    """
    bip_tmark = BuiltInParameter.ALL_MODEL_TYPE_MARK
    bip_descr = BuiltInParameter.ALL_MODEL_DESCRIPTION

    type_marks = map(lambda et: et.get_Parameter(bip_tmark).AsString(), element_types)
    descriptions = map(lambda et: et.get_Parameter(bip_descr).AsString(), element_types)
    
    set_names = map(lambda tmrk, desc: str.format('{0} - {1}', tmrk, desc), type_marks, descriptions)

    map(lambda et, sn: Element.Name.SetValue(et, sn), element_types, set_names)
