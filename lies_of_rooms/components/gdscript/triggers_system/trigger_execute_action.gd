@tool
extends ITrigger

class_name TriggerExecuteAction

@export var method: StringName

func execute(..._parameters: Array) -> void:
	node.call(method)

func _validate_property(property: Dictionary) -> void:
	
	if not node:
		return
	
	if property.name == "method":
		var methods: Array = node.get_method_list().map(
			func(val: Dictionary) -> StringName:
				return val.name
		)
		
		property.hint = PROPERTY_HINT_ENUM
		property.hint_string = ",".join(methods)
