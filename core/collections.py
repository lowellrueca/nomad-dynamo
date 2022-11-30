import clr

clr.AddReference('RevitAPI')
clr.AddReference('RevitServices')

from System.Collections.Generic import List
from Autodesk.Revit.DB import *
from RevitServices.Persistence import DocumentManager as DM


__document__ = DM.Instance.CurrentDBDocument

def collect_families_by_category(category):
    """
    Retrieves the families by category

    :param category: the family's category
    :returns: families of the category
    """
    families = FilteredElementCollector(__document__).OfClass(Family).WhereElementIsNotElementType()
    return filter(lambda f: f.FamilyCategoryId.Equals(category.Id), families)

def collect_element_types_by_category(category):
    """
    Retrieves the element types by category

    :param category: the element type's category
    :returns: element types of the category
    """
    element_types = FilteredElementCollector(__document__).OfCategoryId(category.Id).WhereElementIsElementType()
    return element_types

def collect_elements_by_category(category):
    """
    Retrieves the elements by category

    :param category: the element's category
    :returns: elements of the category
    """
    elements = FilteredElementCollector(__document__).OfCategoryId(category.Id).WhereElementIsNotElementType().ToElements()
    return elements

def element_types_of_family(family):
    """
    Retrieves the element types of the family

    :param family: the family element
    :returns: list of element types of the family
    """
    cat = family.FamilyCategory
    family_types = collect_element_types_by_category(cat)
    return filter(lambda ft: ft.Family.Id.Equals(family.Id), family_types)

def element_types_of_elements(elements):
    """
    Retrieves the element types of the elements

    :param elements: the list of elements
    :returns: the list of element type of the elements
    """
    element_ids = List[ElementId](map(lambda element: element.GetTypeId(), elements))
    return FilteredElementCollector(__document__, element_ids).WhereElementIsElementType().ToElements()

def elements_of_element_types(element_type):
    """
    Retrieves the elements of an element type

    :param element_type: the element type
    :returns: a list of elements of an element type
    """
    elements = FilteredElementCollector(__document__).OfCategoryId(element_type.Category.Id).WhereElementIsNotElementType().ToElements()
    return filter(lambda element: element.GetTypeId().Equals(element_type.Id), elements)
